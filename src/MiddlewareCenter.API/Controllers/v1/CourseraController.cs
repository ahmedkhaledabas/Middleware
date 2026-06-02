using MiddlewareCenter.Application.DTOs.Common;
using MiddlewareCenter.Application.DTOs.Coursera;
using MiddlewareCenter.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MiddlewareCenter.API.Controllers.v1;

/// <summary>
/// Coursera LMS integration endpoints.
/// </summary>
[ApiController]
[Route("api/v1/coursera")]
public class CourseraController : ApiControllerBase
{
    private readonly ICourseraApiClient _client;
    private readonly ILogger<CourseraController> _logger;

    public CourseraController(ICourseraApiClient client, ILogger<CourseraController> logger)
    {
        _client = client;
        _logger = logger;
    }

    /// <summary>Gets all available courses.</summary>
    [HttpGet("courses")]
    [ProducesResponseType(typeof(ApiResponse<List<CourseraCourseDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<List<CourseraCourseDto>>>> GetCourses(CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → GetCourses", TransactionId, CurrentUser);
        var data = await _client.GetCoursesAsync(ct);
        return OkResponse(data);
    }

    /// <summary>Gets a course by ID.</summary>
    [HttpGet("courses/{courseId}")]
    [ProducesResponseType(typeof(ApiResponse<CourseraCourseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<CourseraCourseDto>>> GetCourse(string courseId, CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → GetCourse {CourseId}", TransactionId, CurrentUser, courseId);
        var data = await _client.GetCourseByIdAsync(courseId, ct);
        return OkResponse(data);
    }

    /// <summary>Gets progress for a course.</summary>
    [HttpGet("courses/{courseId}/progress")]
    [ProducesResponseType(typeof(ApiResponse<CourseProgressDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<CourseProgressDto>>> GetProgress(string courseId, CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → GetCourseProgress {CourseId}", TransactionId, CurrentUser, courseId);
        var data = await _client.GetCourseProgressAsync(courseId, ct);
        return OkResponse(data);
    }

    /// <summary>Gets course content/modules.</summary>
    [HttpGet("courses/{courseId}/content")]
    [ProducesResponseType(typeof(ApiResponse<List<CourseModuleDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<List<CourseModuleDto>>>> GetContent(string courseId, CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → GetCourseContent {CourseId}", TransactionId, CurrentUser, courseId);
        var data = await _client.GetCourseContentAsync(courseId, ct);
        return OkResponse(data);
    }

    /// <summary>Gets quizzes for a course.</summary>
    [HttpGet("courses/{courseId}/quizzes")]
    [ProducesResponseType(typeof(ApiResponse<List<QuizDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<List<QuizDto>>>> GetQuizzes(string courseId, CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → GetCourseQuizzes {CourseId}", TransactionId, CurrentUser, courseId);
        var data = await _client.GetCourseQuizzesAsync(courseId, ct);
        return OkResponse(data);
    }

    /// <summary>Submits a quiz attempt.</summary>
    [HttpPost("courses/{courseId}/quizzes/{quizId}/submit")]
    [ProducesResponseType(typeof(ApiResponse<QuizResultDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<QuizResultDto>>> SubmitQuiz(
        string courseId, string quizId, [FromBody] QuizSubmission submission, CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → SubmitQuiz {CourseId}/{QuizId}", TransactionId, CurrentUser, courseId, quizId);
        var data = await _client.SubmitQuizAsync(courseId, quizId, submission, ct);
        return OkResponse(data);
    }
}
