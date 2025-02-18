// Copyright (c) Microsoft. All rights reserved.

using System.IO;
using System.Threading.Tasks;
using Microsoft.SemanticKernel;
using Xunit;

namespace SemanticKernel.Functions.UnitTests.OpenApi;

public sealed class CopilotAgentPluginKernelExtensionsTests
{
    [Fact]
    public async Task ItCanImportPluginFromCopilotAgentPluginAsync()
    {
        // Act
        var kernel = new Kernel();
        var testPluginsDir = Path.Combine(Directory.GetCurrentDirectory(), "OpenApi", "TestPlugins");
        var manifestFilePath = Path.Combine(testPluginsDir, "messages-apiplugin.json");
        var calendarManifestFilePath = Path.Combine(testPluginsDir, "calendar-apiplugin.json");
        // Arrange
        var plugin = await kernel.ImportPluginFromCopilotAgentPluginAsync("MessagesPlugin", manifestFilePath);
        var plugin2 = await kernel.ImportPluginFromCopilotAgentPluginAsync("Calendar", calendarManifestFilePath);

        // Assert
        Assert.NotNull(plugin);
        Assert.Equal(2, plugin.FunctionCount);
        Assert.Equal(411, plugin["me_sendMail"].Description.Length);
        Assert.Equal(1000, plugin["me_ListMessages"].Description.Length);

        Assert.NotNull(plugin2);
        Assert.Equal(4, plugin2.FunctionCount);
    }
}
