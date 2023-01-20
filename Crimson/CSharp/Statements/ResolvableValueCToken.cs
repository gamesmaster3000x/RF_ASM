using Crimson.AntlrBuild;
using Crimson.CSharp.Core;
using Crimson.CSharp.Exception;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Core.Statements;
using System.Net.Http;

namespace Crimson.CSharp.Statements
{
    public class ResolvableValueCToken: ICrimsonToken
    {
        private string? _stringContent;
        private FunctionCallCStatement? _functionContent;
        private ValueType _valueType;

        public ValueType TypeOfValue { get => _valueType; }
        public FunctionCallCStatement? FunctionContent { get => _functionContent; }
        public string? StringContent { get => _stringContent; }

        public ResolvableValueCToken(string? stringContent, ResolvableValueCToken.ValueType valueType)
        {
            _stringContent = stringContent;
            _valueType = valueType;
        }

        public ResolvableValueCToken(FunctionCallCStatement functionContent, ResolvableValueCToken.ValueType valueType)
        {
            _functionContent = functionContent;
            _valueType = valueType;
        }

        /*
         * if ( func(7) > ( 5 + otherfunc(2) ) )
         * 
         * var rval_0 = func(7)
         * var rval_1 = 5 + otherfunc(2)
         * var rval_2 = cond_0 > cond_1
         * 
         * var rval_0 = func(7)
         * var rval_3 = otherfunc(2)
         * var rval_1 = 5 + cond_2
         * var rval_2 = cond_0 > cond_1
         * 
         */
        public Fragment GetCrimsonBasic()
        {
            Fragment f = new Fragment(0);

            switch (_valueType)
            {
                case ValueType.FUNCTION_CALL:
                    return GetBasicAsFunctionCall();
                case ValueType.IDENTIFIER:
                    return GetBasicAsIdentifier();
                case ValueType.NULL:
                    return GetBasicAsNull();
                case ValueType.BOOLEAN:
                    return GetBasicAsBoolean();
                case ValueType.NUMBER:
                    return GetBasicAsNumber();
                case ValueType.MATHS:
                    return GetBasicAsMaths();
            }

            throw new FlatteningException($"ResolvableValues of type {_valueType} cannot be flattened");
        }

        private Fragment GetBasicAsMaths()
        {
            Fragment fragment = new Fragment(0);
            fragment.ResultHolder = _stringContent;
            return fragment;
        }

        private Fragment GetBasicAsNumber()
        {
            Fragment fragment = new Fragment(0);
            fragment.ResultHolder = _stringContent;
            return fragment;
        }

        private Fragment GetBasicAsBoolean()
        {
            Fragment fragment = new Fragment(0);
            fragment.ResultHolder = _stringContent;
            return fragment;
        }

        private Fragment GetBasicAsNull()
        {
            Fragment fragment = new Fragment(0);
            fragment.ResultHolder = _stringContent;
            return fragment;
        }

        private Fragment GetBasicAsIdentifier()
        {
            Fragment fragment = new Fragment(0);
            fragment.ResultHolder = _stringContent;
            return fragment;
        }

        private Fragment GetBasicAsFunctionCall()
        {
            Fragment functionCallFragment = _functionContent!.GetCrimsonBasic();
            return functionCallFragment;
        }

        public void Link(LinkingContext ctx)
        {
            if (_valueType == ValueType.IDENTIFIER)
            {
                _stringContent = LinkerHelper.LinkIdentifier(_stringContent, ctx);
            } else if (_valueType == ValueType.FUNCTION_CALL)
            {
                _functionContent!.Link(ctx);
            }

            return;
        }

        public enum ValueType
        {
            NULL, 
            BOOLEAN, 
            FUNCTION_CALL, 
            NUMBER, 
            MATHS,
            IDENTIFIER
        }
    }
}