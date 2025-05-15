using Microsoft.AspNetCore.Builder;

namespace Ambev.DeveloperEvaluation.Unit.Fixtures;

public class WebApplicationFixture
{
    public WebApplicationBuilder Builder { get; }

    public WebApplicationFixture()
    {
        Builder = WebApplication.CreateBuilder();
    }

    public WebApplication CreateWebApplication()
    {
        return Builder.Build();
    }
}
