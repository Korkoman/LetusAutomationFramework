using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Serilog.Extensions.Logging;

namespace PlaywrightTests.Utilities { 
    
     public static class AssertionHelpers
    {
        
        public static async Task AssertVisible(IPage page, string selector, ILogger logger, string? message = null)
        {
            var isVisible = await page.Locator(selector).IsVisibleAsync();
            logger.Information("AssertVisible: '{Selector}' => {Result}", selector, isVisible);
            Assert.True(isVisible, message ?? $"El elemento '{selector}' no está visible.");
        }

        public static async Task AssertNotVisible(IPage page, string selector, ILogger logger, string? message = null)
        {
            var isVisible = await page.Locator(selector).IsVisibleAsync();
            logger.Information("AssertNotVisible: '{Selector}' => {Result}", selector, isVisible);
            Assert.False(isVisible, message ?? $"El elemento '{selector}' debería estar oculto.");
        }

        public static async Task AssertEnabled(IPage page, string selector, ILogger logger, string? message = null)
        {
            var isEnabled = await page.Locator(selector).IsEnabledAsync();
            logger.Information("AssertEnabled: '{Selector}' => {Result}", selector, isEnabled);
            Assert.True(isEnabled, message ?? $"El elemento '{selector}' no está habilitado.");
        }

        public static async Task AssertDisabled(IPage page, string selector, ILogger logger, string? message = null)
        {
            var isEnabled = await page.Locator(selector).IsEnabledAsync();
            logger.Information("AssertDisabled: '{Selector}' => {Result}", selector, isEnabled);
            Assert.False(isEnabled, message ?? $"El elemento '{selector}' debería estar deshabilitado.");
        }

        public static async Task AssertChecked(IPage page, string selector, ILogger logger, string? message = null)
        {
            var isChecked = await page.Locator(selector).IsCheckedAsync();
            logger.Information("AssertChecked: '{Selector}' => {Result}", selector, isChecked);
            Assert.True(isChecked, message ?? $"El checkbox '{selector}' no está marcado.");
        }

        public static async Task AssertNotChecked(IPage page, string selector, ILogger logger, string? message = null)
        {
            var isChecked = await page.Locator(selector).IsCheckedAsync();
            logger.Information("AssertNotChecked: '{Selector}' => {Result}", selector, isChecked);
            Assert.False(isChecked, message ?? $"El checkbox '{selector}' debería estar desmarcado.");
        }

        public static async Task AssertHasText(IPage page, string selector, string expectedText, ILogger logger, string? message = null)
        {
            var actualText = await page.Locator(selector).InnerTextAsync();
            logger.Information("AssertHasText: '{Selector}' => '{Actual}' (esperado: '{Expected}')", selector, actualText, expectedText);
            Assert.Equal(expectedText, actualText);
        }

        public static async Task AssertTextContains(IPage page, string selector, string expectedSubstring, ILogger logger, string? message = null)
        {
            var actualText = await page.Locator(selector).InnerTextAsync();
            logger.Information("AssertTextContains: '{Selector}' => '{Actual}' contiene '{Expected}'", selector, actualText, expectedSubstring);
            Assert.Contains(expectedSubstring, actualText);
        }

        public static async Task AssertValueIs(IPage page, string selector, string expectedValue, ILogger logger, string? message = null)
        {
            var actualValue = await page.Locator(selector).InputValueAsync();
            logger.Information("AssertValueIs: '{Selector}' => '{Actual}' (esperado: '{Expected}')", selector, actualValue, expectedValue);
            Assert.Equal(expectedValue, actualValue);
        }

        public static async Task AssertValueContains(IPage page, string selector, string expectedSubstring, ILogger logger, string? message = null)
        {
            var actualValue = await page.Locator(selector).InputValueAsync();
            logger.Information("AssertValueContains: '{Selector}' => '{Actual}' contiene '{Expected}'", selector, actualValue, expectedSubstring);
            Assert.Contains(expectedSubstring, actualValue);
        }

        public static async Task AssertHasAttribute(IPage page, string selector, string attributeName, string expectedValue, ILogger logger, string? message = null)
        {
            var attribute = await page.Locator(selector).GetAttributeAsync(attributeName);
            logger.Information("AssertHasAttribute: '{Selector}'[{Attr}] => '{Actual}' (esperado: '{Expected}')", selector, attributeName, attribute, expectedValue);
            Assert.Equal(expectedValue, attribute);
        }

        public static async Task AssertAttributeContains(IPage page, string selector, string attributeName, string expectedSubstring, ILogger logger, string? message = null)
        {
            var attribute = await page.Locator(selector).GetAttributeAsync(attributeName);
            logger.Information("AssertAttributeContains: '{Selector}'[{Attr}] => '{Actual}' contiene '{Expected}'", selector, attributeName, attribute, expectedSubstring);
            Assert.Contains(expectedSubstring, attribute);
        }

        public static async Task AssertHasClass(IPage page, string selector, string expectedClass, ILogger logger, string? message = null)
        {
            var classAttr = await page.Locator(selector).GetAttributeAsync("class");
            logger.Information("AssertHasClass: '{Selector}' => clases: '{Classes}'", selector, classAttr);
            Assert.Contains(expectedClass, classAttr);
        }

        public static async Task AssertElementCount(IPage page, string selector, int expectedCount, ILogger logger, string? message = null)
        {
            var count = await page.Locator(selector).CountAsync();
            logger.Information("AssertElementCount: '{Selector}' => {ActualCount} elementos (esperado: {ExpectedCount})", selector, count, expectedCount);
            Assert.Equal(expectedCount, count);
        }

        public static async Task AssertTitleIs(IPage page, string expectedTitle, ILogger logger, string? message = null)
        {
            var actualTitle = await page.TitleAsync();
            logger.Information("AssertTitleIs => '{Actual}' (esperado: '{Expected}')", actualTitle, expectedTitle);
            Assert.Equal(expectedTitle, actualTitle);
        }

        public static async Task AssertTitleContains(IPage page, string expectedSubstring, ILogger logger, string? message = null)
        {
            var actualTitle = await page.TitleAsync();
            logger.Information("AssertTitleContains => '{Actual}' contiene '{Expected}'", actualTitle, expectedSubstring);
            Assert.Contains(expectedSubstring, actualTitle);
        }

        public static async Task AssertUrlIs(IPage page, string expectedUrl, ILogger logger, string? message = null)
        {
            var actualUrl = page.Url;
            logger.Information("AssertUrlIs => '{Actual}' (esperado: '{Expected}')", actualUrl, expectedUrl);
            Assert.Equal(expectedUrl, actualUrl);
        }

        public static async Task AssertUrlContains(IPage page, string expectedFragment, ILogger logger, string? message = null)
        {
            var actualUrl = page.Url;
            logger.Information("AssertUrlContains => '{Actual}' contiene '{Expected}'", actualUrl, expectedFragment);
            Assert.Contains(expectedFragment, actualUrl);
        }

        public static async Task AssertIsEmpty(IPage page, string selector, ILogger logger, string? message = null)
        {
            var content = await page.Locator(selector).InnerTextAsync();
            logger.Information("AssertIsEmpty: '{Selector}' => '{Content}'", selector, content);
            Assert.True(string.IsNullOrWhiteSpace(content), message ?? $"El contenido de '{selector}' no está vacío.");
        }

        public static async Task AssertIsNotEmpty(IPage page, string selector, ILogger logger, string? message = null)
        {
            var content = await page.Locator(selector).InnerTextAsync();
            logger.Information("AssertIsNotEmpty: '{Selector}' => '{Content}'", selector, content);
            Assert.False(string.IsNullOrWhiteSpace(content), message ?? $"El contenido de '{selector}' está vacío.");
        }

    }
}
