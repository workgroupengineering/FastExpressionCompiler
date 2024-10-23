﻿using NUnit.Framework;
using System;

#if LIGHT_EXPRESSION
using static FastExpressionCompiler.LightExpression.Expression;
namespace FastExpressionCompiler.LightExpression.IssueTests
#else
using System.Linq.Expressions;
using static System.Linq.Expressions.Expression;
namespace FastExpressionCompiler.IssueTests
#endif
{
    [TestFixture]
    public class Issue44_Conversion_To_Nullable_Throws_Exception : ITest
    {
        public int Run()
        {
            Conversion_to_nullable_should_work_with_null_constructed_with_expressions();
            Conversion_to_nullable_should_work();
            Conversion_to_nullable_should_work_with_null();
            return 3;
        }

        [Test]
        public void Conversion_to_nullable_should_work()
        {
            System.Linq.Expressions.Expression<Func<int?>> sExpression = () => 42;
            var expression = sExpression.FromSysExpression();
            int? answer = expression.CompileFast(true).Invoke();

            Assert.IsTrue(answer.HasValue);
            Assert.AreEqual(42, answer.Value);
        }

        [Test]
        public void Conversion_to_nullable_should_work_with_null()
        {
            System.Linq.Expressions.Expression<Func<int?>> sExpression = () => null;
            var expression = sExpression.FromSysExpression();
            int? answer = expression.CompileFast(true).Invoke();

            Assert.IsFalse(answer.HasValue);
        }

        [Test]
        public void Conversion_to_nullable_should_work_with_null_constructed_with_expressions()
        {
            var expr = Lambda<Func<int?>>(Convert(Constant(null), typeof(int?)));

            expr.PrintCSharp();

            int? answer = expr.CompileFast(true).Invoke();

            Assert.IsFalse(answer.HasValue);
        }
    }
}
