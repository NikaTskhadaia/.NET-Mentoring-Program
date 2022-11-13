using Expressions.Task3.E3SQueryProvider.Models.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Expressions.Task3.E3SQueryProvider
{
    public class ExpressionToFtsRequestTranslator : ExpressionVisitor
    {
        readonly StringBuilder _resultStringBuilder;

        public ExpressionToFtsRequestTranslator()
        {
            _resultStringBuilder = new StringBuilder();
        }

        public string Translate(Expression exp)
        {
            Visit(exp);

            return _resultStringBuilder.ToString();
        }

        #region protected methods

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.DeclaringType == typeof(Queryable)
                && node.Method.Name == "Where")
            {
                var predicate = node.Arguments[1];
                Visit(predicate);

                return node;
            }

            if (node.Method.DeclaringType == typeof(string))
            {
                Visit(node.Object as MemberExpression);

                if (node.Method.Name == "Equals")
                {
                    Visit(node.Arguments[0]);
                    return node;
                }

                var constantExpression = node.Arguments[0] as ConstantExpression;
                StringBuilder sb = new StringBuilder(constantExpression.Value.ToString());

                if (node.Method.Name == "StartsWith")
                {
                    sb.Append('*');
                }

                if (node.Method.Name == "EndsWith")
                {
                    sb.Insert(0, '*');
                }

                if (node.Method.Name == "Contains")
                {
                    sb.Insert(0, '*');
                    sb.Append('*');
                }

                constantExpression = Expression.Constant(sb.ToString(), typeof(string));
                Visit(constantExpression);
                return node;
            }

            return base.VisitMethodCall(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Equal:
                    Visit(node.Right);
                    Visit(node.Left);
                    break;
                case ExpressionType.AndAlso: 
                    var statements = new List<Statement>();
                    Visit(node.Left);
                    statements.Add(new Statement { Query = _resultStringBuilder.ToString() });
                    _resultStringBuilder.Clear();
                    Visit(node.Right);
                    statements.Add(new Statement { Query = _resultStringBuilder.ToString() });
                    _resultStringBuilder.Clear();
                    _resultStringBuilder.Append(JsonConvert.SerializeObject(new { statements = statements }));
                    break;

                default:
                    throw new NotSupportedException($"Operation '{node.NodeType}' is not supported");
            };

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            _resultStringBuilder.Insert(0, node.Member.Name).Insert(node.Member.Name.Length, ":");

            return base.VisitMember(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            _resultStringBuilder.Append("(");
            _resultStringBuilder.Append(node.Value);
            _resultStringBuilder.Append(")");

            return node;
        }

        #endregion
    }
}
