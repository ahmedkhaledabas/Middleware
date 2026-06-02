namespace MiddlewareCenter.Application.DTOs.ERP;

// ── Work From Home ──────────────────────────────────────────────────────────

public class WorkFromHomeDto
{
    public int id { get; set; }
    public string employeeId { get; set; } = string.Empty;
    public string employeeName { get; set; } = string.Empty;
    public DateTime fromDate { get; set; }
    public DateTime toDate { get; set; }
    public string? reason { get; set; }
    public string status { get; set; } = string.Empty;
    public string? approvedBy { get; set; }
    public DateTime? approvedAt { get; set; }
    public DateTime createdAt { get; set; }
}

public class CreateWorkFromHomeRequest
{
    public DateTime fromDate { get; set; }
    public DateTime toDate { get; set; }
    public string reason { get; set; } = string.Empty;
}

// ── Leaves ──────────────────────────────────────────────────────────────────

public class LeaveDto
{
    public int id { get; set; }
    public string employeeId { get; set; } = string.Empty;
    public string employeeName { get; set; } = string.Empty;
    public string leaveType { get; set; } = string.Empty;
    public DateTime fromDate { get; set; }
    public DateTime toDate { get; set; }
    public int totalDays { get; set; }
    public string? reason { get; set; }
    public string status { get; set; } = string.Empty;
    public DateTime createdAt { get; set; }
}

public class LeaveBalanceDto
{
    public string employeeId { get; set; } = string.Empty;
    public int annualLeaveBalance { get; set; }
    public int sickLeaveBalance { get; set; }
    public int casualLeaveBalance { get; set; }
    public int totalUsed { get; set; }
    public int totalRemaining { get; set; }
}

public class CreateLeaveRequest
{
    public string leaveType { get; set; } = string.Empty;
    public DateTime fromDate { get; set; }
    public DateTime toDate { get; set; }
    public string? reason { get; set; }
}

// ── Vacations ────────────────────────────────────────────────────────────────

public class VacationDto
{
    public int id { get; set; }
    public string employeeId { get; set; } = string.Empty;
    public string employeeName { get; set; } = string.Empty;
    public DateTime fromDate { get; set; }
    public DateTime toDate { get; set; }
    public int totalDays { get; set; }
    public string status { get; set; } = string.Empty;
    public DateTime createdAt { get; set; }
}

public class CreateVacationRequest
{
    public DateTime fromDate { get; set; }
    public DateTime toDate { get; set; }
    public string? notes { get; set; }
}

// ── HR Letters ───────────────────────────────────────────────────────────────

public class HrLetterDto
{
    public int id { get; set; }
    public string employeeId { get; set; } = string.Empty;
    public string employeeName { get; set; } = string.Empty;
    public string letterType { get; set; } = string.Empty;
    public string? purpose { get; set; }
    public string status { get; set; } = string.Empty;
    public DateTime requestedAt { get; set; }
    public DateTime? issuedAt { get; set; }
}

public class CreateHrLetterRequest
{
    public string letterType { get; set; } = string.Empty;
    public string purpose { get; set; } = string.Empty;
    public string? addressedTo { get; set; }
}

// ── Work Location ────────────────────────────────────────────────────────────

public class WorkLocationDto
{
    public int id { get; set; }
    public string employeeId { get; set; } = string.Empty;
    public string employeeName { get; set; } = string.Empty;
    public string location { get; set; } = string.Empty;
    public DateTime effectiveDate { get; set; }
    public string status { get; set; } = string.Empty;
    public string? approvedBy { get; set; }
    public DateTime createdAt { get; set; }
}

public class CreateWorkLocationRequest
{
    public string location { get; set; } = string.Empty;
    public DateTime effectiveDate { get; set; }
    public string? reason { get; set; }
}

// ── ID Replacement ───────────────────────────────────────────────────────────

public class IdReplacementDto
{
    public int id { get; set; }
    public string employeeId { get; set; } = string.Empty;
    public string employeeName { get; set; } = string.Empty;
    public string reason { get; set; } = string.Empty;
    public string status { get; set; } = string.Empty;
    public DateTime requestedAt { get; set; }
    public DateTime? processedAt { get; set; }
}

public class CreateIdReplacementRequest
{
    public string reason { get; set; } = string.Empty;
    public string? additionalNotes { get; set; }
}

// ── HR Points ────────────────────────────────────────────────────────────────

public class HrPointsDto
{
    public string employeeId { get; set; } = string.Empty;
    public int totalPoints { get; set; }
    public int usedPoints { get; set; }
    public int availablePoints { get; set; }
    public List<HrPointTransactionDto> transactions { get; set; } = new();
}

public class HrPointTransactionDto
{
    public int id { get; set; }
    public string description { get; set; } = string.Empty;
    public int points { get; set; }
    public string type { get; set; } = string.Empty;
    public DateTime transactionDate { get; set; }
}

// ── HR Requests ──────────────────────────────────────────────────────────────

public class HrRequestDto
{
    public int id { get; set; }
    public string requestType { get; set; } = string.Empty;
    public string requesterId { get; set; } = string.Empty;
    public string requesterName { get; set; } = string.Empty;
    public string? assignedTo { get; set; }
    public string status { get; set; } = string.Empty;
    public string? description { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime? resolvedAt { get; set; }
}
