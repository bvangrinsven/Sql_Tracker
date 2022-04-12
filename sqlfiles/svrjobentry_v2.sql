CREATE TABLE #list_running_SQL_jobs
(
    job_id UNIQUEIDENTIFIER NOT NULL
  , last_run_date INT NOT NULL
  , last_run_time INT NOT NULL
  , next_run_date INT NOT NULL
  , next_run_time INT NOT NULL
  , next_run_schedule_id INT NOT NULL
  , requested_to_run INT NOT NULL
  , request_source INT NOT NULL
  , request_source_id sysname NULL
  , running INT NOT NULL
  , current_step INT NOT NULL
  , current_retry_attempt INT NOT NULL
  , job_state INT NOT NULL
);

DECLARE @sqluser NVARCHAR(128)
      , @is_sysadmin INT;

SELECT @is_sysadmin = ISNULL(IS_SRVROLEMEMBER(N'sysadmin'), 0);

DECLARE read_sysjobs_for_running CURSOR FOR
    SELECT DISTINCT SUSER_SNAME(owner_sid)FROM msdb.dbo.sysjobs;
OPEN read_sysjobs_for_running;
FETCH NEXT FROM read_sysjobs_for_running
INTO @sqluser;

WHILE @@FETCH_STATUS = 0
BEGIN
    INSERT INTO #list_running_SQL_jobs
    EXECUTE master.dbo.xp_sqlagent_enum_jobs @is_sysadmin, @sqluser;
    FETCH NEXT FROM read_sysjobs_for_running
    INTO @sqluser;
END;

CLOSE read_sysjobs_for_running;
DEALLOCATE read_sysjobs_for_running;

SELECT j.job_id
	 , j.name
     , 'Enbld' = CASE j.enabled
                     WHEN 0
                         THEN 'no'
                     ELSE 'YES'
                 END
     , 'Min' = DATEDIFF(MINUTE, a.start_execution_date, ISNULL(a.stop_execution_date, GETDATE()))
     , 'Status' = CASE
                      WHEN a.start_execution_date IS NOT NULL
                          AND a.stop_execution_date IS NULL
                          THEN 'Executing'
                      WHEN h.run_status = 0
                          THEN 'FAILED'
                      WHEN h.run_status = 2
                          THEN 'Retry'
                      WHEN h.run_status = 3
                          THEN 'Canceled'
                      WHEN h.run_status = 4
                          THEN 'InProg'
                      WHEN h.run_status = 1
                          THEN 'Success'
                      ELSE 'Idle'
                  END
     , r.current_step
     , spid = p.session_id
     , owner = ISNULL(SUSER_SNAME(j.owner_sid), 'S-' + CONVERT(NVARCHAR(12), CONVERT(BIGINT, UNICODE(LEFT(CONVERT(NVARCHAR(256), j.owner_sid), 1))) - CONVERT(BIGINT, 256) * CONVERT(BIGINT, UNICODE(LEFT(CONVERT(NVARCHAR(256), j.owner_sid), 1)) / 256)) + '-' + CONVERT(NVARCHAR(12), UNICODE(RIGHT(LEFT(CONVERT(NVARCHAR(256), j.owner_sid), 4), 1)) / 256 + CONVERT(BIGINT, NULLIF(UNICODE(LEFT(CONVERT(NVARCHAR(256), j.owner_sid), 1)) / 256, 0)) - CONVERT(BIGINT, UNICODE(LEFT(CONVERT(NVARCHAR(256), j.owner_sid), 1)) / 256)) + ISNULL('-' + CONVERT(NVARCHAR(12), CONVERT(BIGINT, UNICODE(RIGHT(LEFT(CONVERT(NVARCHAR(256), j.owner_sid), 5), 1))) + CONVERT(BIGINT, UNICODE(RIGHT(LEFT(CONVERT(NVARCHAR(256), j.owner_sid), 6), 1))) * CONVERT(BIGINT, 65536) + CONVERT(BIGINT, NULLIF(SIGN(LEN(CONVERT(NVARCHAR(256), j.owner_sid)) - 6), -1)) * 0), '') + ISNULL('-' + CONVERT(NVARCHAR(12), CONVERT(BIGINT, UNICODE(RIGHT(LEFT(CONVERT(NVARCHAR(256), j.owner_sid), 7), 1))) + CONVERT(BIGINT, UNICODE(RIGHT(LEFT(CONVERT(NVARCHAR(256), j.owner_sid), 8), 1))) * CONVERT(BIGINT, 65536) + CONVERT(BIGINT, NULLIF(SIGN(LEN(CONVERT(NVARCHAR(256), j.owner_sid)) - 8), -1)) * 0), '') + ISNULL('-' + CONVERT(NVARCHAR(12), CONVERT(BIGINT, UNICODE(RIGHT(LEFT(CONVERT(NVARCHAR(256), j.owner_sid), 9), 1))) + CONVERT(BIGINT, UNICODE(RIGHT(LEFT(CONVERT(NVARCHAR(256), j.owner_sid), 10), 1))) * CONVERT(BIGINT, 65536) + CONVERT(BIGINT, NULLIF(SIGN(LEN(CONVERT(NVARCHAR(256), j.owner_sid)) - 10), -1)) * 0), '') + ISNULL('-' + CONVERT(NVARCHAR(12), CONVERT(BIGINT, UNICODE(RIGHT(LEFT(CONVERT(NVARCHAR(256), j.owner_sid), 11), 1))) + CONVERT(BIGINT, UNICODE(RIGHT(LEFT(CONVERT(NVARCHAR(256), j.owner_sid), 12), 1))) * CONVERT(BIGINT, 65536) + CONVERT(BIGINT, NULLIF(SIGN(LEN(CONVERT(NVARCHAR(256), j.owner_sid)) - 12), -1)) * 0), '') + ISNULL('-' + CONVERT(NVARCHAR(12), CONVERT(BIGINT, UNICODE(RIGHT(LEFT(CONVERT(NVARCHAR(256), j.owner_sid), 13), 1))) + CONVERT(BIGINT, UNICODE(RIGHT(LEFT(CONVERT(NVARCHAR(256), j.owner_sid), 14), 1))) * CONVERT(BIGINT, 65536) + CONVERT(BIGINT, NULLIF(SIGN(LEN(CONVERT(NVARCHAR(256), j.owner_sid)) - 14), -1)) * 0), '')) --SHOW as NT SID when unresolved
     , a.start_execution_date
     , a.stop_execution_date
     , t.subsystem
     , t.step_name
FROM msdb.dbo.sysjobs j
    LEFT OUTER JOIN (SELECT DISTINCT * FROM #list_running_SQL_jobs) r
        ON j.job_id = r.job_id
    LEFT OUTER JOIN msdb.dbo.sysjobactivity a
        ON j.job_id = a.job_id
            AND a.start_execution_date IS NOT NULL
            --AND a.stop_execution_date IS NULL
            AND NOT EXISTS
            (
                SELECT *
                FROM msdb.dbo.sysjobactivity at
                WHERE at.job_id = a.job_id
                    AND at.start_execution_date > a.start_execution_date
            )
    LEFT OUTER JOIN sys.dm_exec_sessions p
        ON p.program_name LIKE 'SQLAgent%0x%'
            AND j.job_id = SUBSTRING(SUBSTRING(p.program_name, CHARINDEX('0x', p.program_name) + 2, 32), 7, 2) + SUBSTRING(SUBSTRING(p.program_name, CHARINDEX('0x', p.program_name) + 2, 32), 5, 2) + SUBSTRING(SUBSTRING(p.program_name, CHARINDEX('0x', p.program_name) + 2, 32), 3, 2) + SUBSTRING(SUBSTRING(p.program_name, CHARINDEX('0x', p.program_name) + 2, 32), 1, 2) + '-' + SUBSTRING(SUBSTRING(p.program_name, CHARINDEX('0x', p.program_name) + 2, 32), 11, 2) + SUBSTRING(SUBSTRING(p.program_name, CHARINDEX('0x', p.program_name) + 2, 32), 9, 2) + '-' + SUBSTRING(SUBSTRING(p.program_name, CHARINDEX('0x', p.program_name) + 2, 32), 15, 2) + SUBSTRING(SUBSTRING(p.program_name, CHARINDEX('0x', p.program_name) + 2, 32), 13, 2) + '-' + SUBSTRING(SUBSTRING(p.program_name, CHARINDEX('0x', p.program_name) + 2, 32), 17, 4) + '-' + SUBSTRING(SUBSTRING(p.program_name, CHARINDEX('0x', p.program_name) + 2, 32), 21, 12)
    LEFT OUTER JOIN msdb.dbo.sysjobhistory h
        ON j.job_id = h.job_id
            AND h.instance_id = a.job_history_id
    LEFT OUTER JOIN msdb.dbo.sysjobsteps t
        ON t.job_id = j.job_id
            AND t.step_id = r.current_step
ORDER BY 1;

DROP TABLE #list_running_SQL_jobs;