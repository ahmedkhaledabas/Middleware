using MiddlewareCenter.Application.DTOs.Coursera;
using MiddlewareCenter.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace MiddlewareCenter.Infrastructure.Clients;

/// <summary>
/// Coursera LMS API client implementation.
/// </summary>
public class CourseraApiClient : ExternalApiClientBase, ICourseraApiClient
{
    public CourseraApiClient(HttpClient httpClient, string baseUrl, ILogger<CourseraApiClient> logger)
        : base(httpClient, baseUrl, logger) { }

    public Task<List<CourseraCourseDto>> GetCoursesAsync(CancellationToken ct = default)
        => GetAsync<List<CourseraCourseDto>>("courses", ct);

    public Task<CourseraCourseDto> GetCourseByIdAsync(string courseId, CancellationToken ct = default)
        => GetAsync<CourseraCourseDto>($"courses/{courseId}", ct);

    public Task<CourseProgressDto> GetCourseProgressAsync(string courseId, CancellationToken ct = default)
        => GetAsync<CourseProgressDto>($"courses/{courseId}/progress", ct);

    public Task<List<CourseModuleDto>> GetCourseContentAsync(string courseId, CancellationToken ct = default)
        => GetAsync<List<CourseModuleDto>>($"courses/{courseId}/content", ct);

    public Task<List<QuizDto>> GetCourseQuizzesAsync(string courseId, CancellationToken ct = default)
        => GetAsync<List<QuizDto>>($"courses/{courseId}/quizzes", ct);

    public Task<QuizResultDto> SubmitQuizAsync(string courseId, string quizId, QuizSubmission submission, CancellationToken ct = default)
        => PostAsync<QuizSubmission, QuizResultDto>($"courses/{courseId}/quizzes/{quizId}/submit", submission, ct);
}
