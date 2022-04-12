SELECT sj.job_id, sj.[name],
    CASE
        WHEN sja.start_execution_date IS NULL THEN 'Never ran'
        WHEN sja.start_execution_date IS NOT NULL AND sja.stop_execution_date IS NULL THEN 'Running'
        WHEN sja.start_execution_date IS NOT NULL AND sja.stop_execution_date IS NOT NULL THEN 'Not running'
    END AS 'RunStatus',
    CASE WHEN sja.start_execution_date IS NOT NULL AND sja.stop_execution_date IS NULL then js.StepCount else null end As TotalNumberOfSteps,
    CASE WHEN sja.start_execution_date IS NOT NULL AND sja.stop_execution_date IS NULL then ISNULL(sja.last_executed_step_id+1,js.StepCount) else null end as currentlyExecutingStep,
    CASE WHEN sja.start_execution_date IS NOT NULL AND sja.stop_execution_date IS NULL then datediff(minute, sja.run_requested_date, getdate()) ELSE NULL end as ElapsedTime
FROM msdb.dbo.sysjobs sj
JOIN msdb.dbo.sysjobactivity sja
ON sj.job_id = sja.job_id
CROSS APPLY (SELECT COUNT(*) FROM msdb.dbo.sysjobsteps as js WHERE js.job_id = sj.job_id) as js(StepCount)
WHERE session_id = (
    SELECT MAX(session_id) FROM msdb.dbo.sysjobactivity)
ORDER BY RunStatus desc


