namespace ILS.RfqManagement.DomainModel
{
    /// <summary>
    /// Represents the after-hours escalation settings for an assignment rule.
    /// </summary>
    public class AssignmentEscalation
    {
        /// <summary>
        /// Gets or sets a value indicating whether after-hours escalation is enabled.
        /// </summary>
        /// <value>
        /// True if escalation is enabled.
        /// </value>
        public bool EscalationEnabled { get; set; }

        /// <summary>
        /// Gets or sets who to escalate to after-hours.
        /// </summary>
        /// <value>
        /// The escalation administrator identifier.
        /// </value>
        public string EscalationAdministratorId { get; set; }

        /// <summary>
        /// Gets or sets the after-hours window start time, formatted HH:MI AM/PM.
        /// </summary>
        /// <value>
        /// The start time.
        /// </value>
        public string StartTime { get; set; }

        /// <summary>
        /// Gets or sets the after-hours window end time, formatted HH:MI AM/PM.
        /// </summary>
        /// <value>
        /// The end time.
        /// </value>
        public string EndTime { get; set; }

        /// <summary>
        /// Gets or sets the time zone the start/end time window is evaluated in.
        /// </summary>
        /// <value>
        /// The time zone.
        /// </value>
        public string TimeZone { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether an email notification is sent on escalation.
        /// </summary>
        /// <value>
        /// True to notify by email.
        /// </value>
        public bool NotifyByEmail { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether escalation applies on Monday.
        /// </summary>
        /// <value>
        /// True if escalation applies on Monday.
        /// </value>
        public bool Mon { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether escalation applies on Tuesday.
        /// </summary>
        /// <value>
        /// True if escalation applies on Tuesday.
        /// </value>
        public bool Tue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether escalation applies on Wednesday.
        /// </summary>
        /// <value>
        /// True if escalation applies on Wednesday.
        /// </value>
        public bool Wed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether escalation applies on Thursday.
        /// </summary>
        /// <value>
        /// True if escalation applies on Thursday.
        /// </value>
        public bool Thu { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether escalation applies on Friday.
        /// </summary>
        /// <value>
        /// True if escalation applies on Friday.
        /// </value>
        public bool Fri { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether escalation applies on Saturday.
        /// </summary>
        /// <value>
        /// True if escalation applies on Saturday.
        /// </value>
        public bool Sat { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether escalation applies on Sunday.
        /// </summary>
        /// <value>
        /// True if escalation applies on Sunday.
        /// </value>
        public bool Sun { get; set; }
    }
}
