
namespace bentran1vn.question.src.Middlewares
{
    public class RequestPipelineStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return builder =>
            {
                builder.UseAuthentication();
                builder.UseAuthorization();
                next(builder);
            };
        }
    }
}
