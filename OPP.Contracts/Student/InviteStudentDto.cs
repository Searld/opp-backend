namespace OPP.Contracts.Student;

public record InviteStudentDto(string Email, Guid StudentId, Guid ProjectId);