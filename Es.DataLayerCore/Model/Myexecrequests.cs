using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    [Table("myexecrequests")]
    public partial class Myexecrequests
    {
        [Column("session_id")]
        public short SessionId { get; set; }
        [Column("request_id")]
        public int RequestId { get; set; }
        [Column("start_time", TypeName = "datetime")]
        public DateTime StartTime { get; set; }
        [Required]
        [Column("status")]
        [StringLength(30)]
        public string Status { get; set; }
        [Required]
        [Column("command")]
        [StringLength(32)]
        public string Command { get; set; }
        [Column("sql_handle")]
        [MaxLength(64)]
        public byte[] SqlHandle { get; set; }
        [Column("statement_start_offset")]
        public int? StatementStartOffset { get; set; }
        [Column("statement_end_offset")]
        public int? StatementEndOffset { get; set; }
        [Column("plan_handle")]
        [MaxLength(64)]
        public byte[] PlanHandle { get; set; }
        [Column("database_id")]
        public short DatabaseId { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("connection_id")]
        public Guid? ConnectionId { get; set; }
        [Column("blocking_session_id")]
        public short? BlockingSessionId { get; set; }
        [Column("wait_type")]
        [StringLength(60)]
        public string WaitType { get; set; }
        [Column("wait_time")]
        public int WaitTime { get; set; }
        [Required]
        [Column("last_wait_type")]
        [StringLength(60)]
        public string LastWaitType { get; set; }
        [Required]
        [Column("wait_resource")]
        [StringLength(256)]
        public string WaitResource { get; set; }
        [Column("open_transaction_count")]
        public int OpenTransactionCount { get; set; }
        [Column("open_resultset_count")]
        public int OpenResultsetCount { get; set; }
        [Column("transaction_id")]
        public long TransactionId { get; set; }
        [Column("context_info")]
        [MaxLength(128)]
        public byte[] ContextInfo { get; set; }
        [Column("percent_complete")]
        public float PercentComplete { get; set; }
        [Column("estimated_completion_time")]
        public long EstimatedCompletionTime { get; set; }
        [Column("cpu_time")]
        public int CpuTime { get; set; }
        [Column("total_elapsed_time")]
        public int TotalElapsedTime { get; set; }
        [Column("scheduler_id")]
        public int? SchedulerId { get; set; }
        [Column("task_address")]
        [MaxLength(8)]
        public byte[] TaskAddress { get; set; }
        [Column("reads")]
        public long Reads { get; set; }
        [Column("writes")]
        public long Writes { get; set; }
        [Column("logical_reads")]
        public long LogicalReads { get; set; }
        [Column("text_size")]
        public int TextSize { get; set; }
        [Column("language")]
        [StringLength(128)]
        public string Language { get; set; }
        [Column("date_format")]
        [StringLength(3)]
        public string DateFormat { get; set; }
        [Column("date_first")]
        public short DateFirst { get; set; }
        [Column("quoted_identifier")]
        public bool QuotedIdentifier { get; set; }
        [Column("arithabort")]
        public bool Arithabort { get; set; }
        [Column("ansi_null_dflt_on")]
        public bool AnsiNullDfltOn { get; set; }
        [Column("ansi_defaults")]
        public bool AnsiDefaults { get; set; }
        [Column("ansi_warnings")]
        public bool AnsiWarnings { get; set; }
        [Column("ansi_padding")]
        public bool AnsiPadding { get; set; }
        [Column("ansi_nulls")]
        public bool AnsiNulls { get; set; }
        [Column("concat_null_yields_null")]
        public bool ConcatNullYieldsNull { get; set; }
        [Column("transaction_isolation_level")]
        public short TransactionIsolationLevel { get; set; }
        [Column("lock_timeout")]
        public int LockTimeout { get; set; }
        [Column("deadlock_priority")]
        public int DeadlockPriority { get; set; }
        [Column("row_count")]
        public long RowCount { get; set; }
        [Column("prev_error")]
        public int PrevError { get; set; }
        [Column("nest_level")]
        public int NestLevel { get; set; }
        [Column("granted_query_memory")]
        public int GrantedQueryMemory { get; set; }
        [Column("executing_managed_code")]
        public bool ExecutingManagedCode { get; set; }
        [Column("group_id")]
        public int GroupId { get; set; }
        [Column("query_hash")]
        [MaxLength(8)]
        public byte[] QueryHash { get; set; }
        [Column("query_plan_hash")]
        [MaxLength(8)]
        public byte[] QueryPlanHash { get; set; }
        [Column("statement_sql_handle")]
        [MaxLength(64)]
        public byte[] StatementSqlHandle { get; set; }
        [Column("statement_context_id")]
        public long? StatementContextId { get; set; }
        [Column("dop")]
        public int Dop { get; set; }
        [Column("parallel_worker_count")]
        public int? ParallelWorkerCount { get; set; }
        [Column("external_script_request_id")]
        public Guid? ExternalScriptRequestId { get; set; }
        [Column("is_resumable")]
        public bool IsResumable { get; set; }
        [Column("page_resource")]
        [MaxLength(8)]
        public byte[] PageResource { get; set; }
        [Column("page_server_reads")]
        public long PageServerReads { get; set; }
    }
}
