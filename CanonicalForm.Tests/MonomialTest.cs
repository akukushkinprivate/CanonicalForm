// <copyright file="MonomialTest.cs">Copyright ©  2016</copyright>
using System;
using CanonicalForm;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CanonicalForm.Tests
{
    /// <summary>This class contains parameterized unit tests for Monomial</summary>
    [PexClass(typeof(Monomial))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class MonomialTest
    {
    }
}
