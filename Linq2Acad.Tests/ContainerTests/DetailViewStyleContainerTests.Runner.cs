﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public partial class DetailViewStyleContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestCreateDetailViewStyle()
    {
      var result = AcadTestRunner.TestRunner.RunTest<DetailViewStyleContainerTests>("CreateDetailViewStyle");
      if (!result.Passed) Assert.Fail(result.Message);
    }
  }
}
