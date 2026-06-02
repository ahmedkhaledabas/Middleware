using MiddlewareCenter.Application.DTOs.Coursera;
using MiddlewareCenter.Domain.Interfaces;

namespace MiddlewareCenter.Application.Interfaces;

/// <summary>
/// Coursera LMS integration client.
/// </summary>
public interface ICourseraApiClient : IExternalApiClient
{
    Task<List<CourseraCourseDto>> GetCoursesAsync(CancellationToken ct = default);
    Task<CourseraCourseDto> GetCourseByIdAsync(string courseId, CancellationToken ct = default);
    Task<CourseProgressDto> GetCourseProgressAsync(string courseId, CancellationToken ct = default);
    Task<List<CourseModuleDto>> GetCourseContentAsync(string courseId, CancellationToken ct = default);
    Task<List<QuizDto>> GetCourseQuizzesAsync(string courseId, CancellationToken ct = default);
    Task<QuizResultDto> SubmitQuizAsync(string courseId, string quizId, QuizSubmission submission, CancellationToken ct = default);
}
