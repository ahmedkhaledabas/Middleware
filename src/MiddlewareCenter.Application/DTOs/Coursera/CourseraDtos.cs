namespace MiddlewareCenter.Application.DTOs.Coursera;

public class CourseraCourseDto
{
    public string courseId { get; set; } = string.Empty;
    public string title { get; set; } = string.Empty;
    public string? description { get; set; }
    public string? instructor { get; set; }
    public string? category { get; set; }
    public string? level { get; set; }
    public double? rating { get; set; }
    public int? enrolledCount { get; set; }
    public string? thumbnailUrl { get; set; }
    public DateTime? startDate { get; set; }
    public DateTime? endDate { get; set; }
    public bool isEnrolled { get; set; }
}

public class CourseProgressDto
{
    public string courseId { get; set; } = string.Empty;
    public string userId { get; set; } = string.Empty;
    public double progressPercentage { get; set; }
    public int completedModules { get; set; }
    public int totalModules { get; set; }
    public DateTime? lastAccessedAt { get; set; }
    public bool isCompleted { get; set; }
    public DateTime? completedAt { get; set; }
}

public class CourseModuleDto
{
    public string moduleId { get; set; } = string.Empty;
    public string title { get; set; } = string.Empty;
    public int order { get; set; }
    public bool isCompleted { get; set; }
    public List<LessonDto> lessons { get; set; } = new();
}

public class LessonDto
{
    public string lessonId { get; set; } = string.Empty;
    public string title { get; set; } = string.Empty;
    public string? type { get; set; }
    public int durationMinutes { get; set; }
    public bool isCompleted { get; set; }
    public string? contentUrl { get; set; }
}

public class QuizDto
{
    public string quizId { get; set; } = string.Empty;
    public string title { get; set; } = string.Empty;
    public int totalQuestions { get; set; }
    public int timeLimitMinutes { get; set; }
    public double passingScore { get; set; }
    public List<QuizQuestionDto> questions { get; set; } = new();
}

public class QuizQuestionDto
{
    public string questionId { get; set; } = string.Empty;
    public string questionText { get; set; } = string.Empty;
    public string questionType { get; set; } = string.Empty;
    public List<string> options { get; set; } = new();
}

public class QuizSubmission
{
    public Dictionary<string, string> answers { get; set; } = new();
    public DateTime submittedAt { get; set; } = DateTime.UtcNow;
}

public class QuizResultDto
{
    public string quizId { get; set; } = string.Empty;
    public string userId { get; set; } = string.Empty;
    public double score { get; set; }
    public bool passed { get; set; }
    public int correctAnswers { get; set; }
    public int totalQuestions { get; set; }
    public DateTime submittedAt { get; set; }
}
