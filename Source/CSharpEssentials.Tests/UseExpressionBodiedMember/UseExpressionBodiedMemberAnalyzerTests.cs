﻿using CSharpEssentials.UseExpressionBodiedMember;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using NUnit.Framework;
using RoslynNUnitLight;

namespace CSharpEssentials.Tests.UseExpressionBodiedMember
{
    [TestFixture]
    public class UseExpressionBodiedMemberAnalyzerTests : AnalyzerTestFixture
    {
        protected override string LanguageName => LanguageNames.CSharp;
        protected override DiagnosticAnalyzer CreateAnalyzer() => new UseExpressionBodiedMemberAnalyzer();

        [Test]
        public void NoDiagnosticWhenThereIsAnAttributeOnAnAccessor()
        {
            const string code = @"
class C
{
    int Property
    {
        [A] get { return 42; }
    }
}";

            NoDiagnostic(code, DiagnosticIds.UseExpressionBodiedMember);
        }

        private void VerifyNotAvailableInGeneratedCode(string filePath)
        {
            const string code = @"
class C
{
    int Property
    {
        get { return 42; }
    }
}";

            var document = TestHelpers
                .GetDocument(code, this.LanguageName)
                .WithFilePath(filePath);

            NoDiagnostic(document, DiagnosticIds.UseExpressionBodiedMember);
        }

        [Test]
        public void NotAvailableInGeneratedCode1()
        {
            VerifyNotAvailableInGeneratedCode("TemporaryGeneratedFile_TestDocument.cs");
        }

        [Test]
        public void NotAvailableInGeneratedCode2()
        {
            VerifyNotAvailableInGeneratedCode("AssemblyInfo.cs");
        }

        [Test]
        public void NotAvailableInGeneratedCode3()
        {
            VerifyNotAvailableInGeneratedCode("TestDocument.designer.cs");
        }

        [Test]
        public void NotAvailableInGeneratedCode4()
        {
            VerifyNotAvailableInGeneratedCode("TestDocument.g.cs");
        }

        [Test]
        public void NotAvailableInGeneratedCode5()
        {
            VerifyNotAvailableInGeneratedCode("TestDocument.g.i.cs");
        }

        [Test]
        public void NotAvailableInGeneratedCode6()
        {
            VerifyNotAvailableInGeneratedCode("TestDocument.AssemblyAttributes.cs");
        }
    }
}
