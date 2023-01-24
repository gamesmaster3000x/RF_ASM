using Crimson.AntlrBuild;
using Crimson.CSharp.Core;
using Crimson.CSharp.Exception;
using Crimson.CSharp.Grammar.Statements;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Statements;
using System.Net.Http;

namespace Crimson.CSharp.Grammar.Tokens
{
    public abstract class ResolvableValueCToken : ICrimsonToken
    {
        private string? _stringContent;
        private FunctionCallCStatement? _functionContent;
        private ValueType _valueType;

        public ValueType TypeOfValue { get => _valueType; }
        public FunctionCallCStatement? FunctionContent { get => _functionContent; }
        public string? StringContent { get => _stringContent; }

        public ResolvableValueCToken(string? stringContent, ValueType valueType)
        {
            _stringContent = stringContent;
            _valueType = valueType;
        }

        public ResolvableValueCToken(FunctionCallCStatement functionContent, ValueType valueType)
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
        public virtual Fragment GetCrimsonBasic()
        {
            Fragment f = new Fragment(0);

            switch (_valueType)
            {
                case ValueType.IDENTIFIER:
                    return GetBasicAsIdentifier();
                case ValueType.OPERATION:
                    return GetBasicAsOperation();
                case ValueType.FUNCTION_CALL:
                    return GetBasicAsFunctionCall();
                case ValueType.RAW:
                    return GetBasicAsRaw();
            }

            throw new FlatteningException($"ResolvableValues of type {_valueType} cannot be flattened");
        }

        private Fragment GetBasicAsIdentifier()
        {
            Fragment fragment = new Fragment(0);
            fragment.ResultHolder = _stringContent;
            return fragment;
        }

        private Fragment GetBasicAsOperation()
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

        private Fragment GetBasicAsRaw()
        {
            Fragment fragment = new Fragment(0);
            fragment.ResultHolder = _stringContent;
            return fragment;
        }

        public void Link(LinkingContext ctx)
        {
            if (_valueType == ValueType.IDENTIFIER)
            {
                _stringContent = LinkerHelper.LinkIdentifier(_stringContent, ctx);
            }
            else if (_valueType == ValueType.FUNCTION_CALL)
            {
                _functionContent!.Link(ctx);
            }

            return;
        }

        public enum ValueType
        {
            IDENTIFIER,
            OPERATION,
            FUNCTION_CALL,
            RAW
        }
    }
}