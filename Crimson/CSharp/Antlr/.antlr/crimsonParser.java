// Generated from c:\Users\GenElectrovise\OneDrive\Desktop\Programming\RF_ASM\Crimson\CSharp\Antlr\crimson.g4 by ANTLR 4.9.2
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;
import java.util.List;
import java.util.Iterator;
import java.util.ArrayList;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class crimsonParser extends Parser {
	static { RuntimeMetaData.checkVersion("4.9.2", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		Package=1, Function=2, Global=3, Return=4, Integer=5, Boolean=6, Equals=7, 
		OpenBracket=8, CloseBracket=9, OpenSquare=10, CloseSquare=11, OpenBrace=12, 
		CloseBrace=13, Comma=14, Dot=15, SemiColon=16, Identifier=17, Value=18;
	public static final int
		RULE_program = 0, RULE_packageDefinitionList = 1, RULE_packageDefinition = 2, 
		RULE_packageDependencyList = 3, RULE_packageDependency = 4, RULE_packageBody = 5, 
		RULE_topLevelStatement = 6, RULE_globalVariableDeclaration = 7, RULE_functionDeclaration = 8, 
		RULE_functionReturnType = 9, RULE_functionBody = 10, RULE_functionOnlyStatement = 11, 
		RULE_internalVariableDeclaration = 12, RULE_assignVariable = 13, RULE_functionCall = 14, 
		RULE_inputParameters = 15, RULE_functionReturn = 16, RULE_resolvableValue = 17, 
		RULE_parameterList = 18, RULE_parameter = 19, RULE_parameterType = 20;
	private static String[] makeRuleNames() {
		return new String[] {
			"program", "packageDefinitionList", "packageDefinition", "packageDependencyList", 
			"packageDependency", "packageBody", "topLevelStatement", "globalVariableDeclaration", 
			"functionDeclaration", "functionReturnType", "functionBody", "functionOnlyStatement", 
			"internalVariableDeclaration", "assignVariable", "functionCall", "inputParameters", 
			"functionReturn", "resolvableValue", "parameterList", "parameter", "parameterType"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "'package'", "'function'", "'global'", "'return'", "'int'", "'bool'", 
			"'='", "'('", "')'", "'['", "']'", "'{'", "'}'", "','", "'.'", "';'"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, "Package", "Function", "Global", "Return", "Integer", "Boolean", 
			"Equals", "OpenBracket", "CloseBracket", "OpenSquare", "CloseSquare", 
			"OpenBrace", "CloseBrace", "Comma", "Dot", "SemiColon", "Identifier", 
			"Value"
		};
	}
	private static final String[] _SYMBOLIC_NAMES = makeSymbolicNames();
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}

	@Override
	public String getGrammarFileName() { return "crimson.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public ATN getATN() { return _ATN; }

	public crimsonParser(TokenStream input) {
		super(input);
		_interp = new ParserATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	public static class ProgramContext extends ParserRuleContext {
		public PackageDefinitionListContext packageDefinitionList() {
			return getRuleContext(PackageDefinitionListContext.class,0);
		}
		public TerminalNode EOF() { return getToken(crimsonParser.EOF, 0); }
		public ProgramContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_program; }
	}

	public final ProgramContext program() throws RecognitionException {
		ProgramContext _localctx = new ProgramContext(_ctx, getState());
		enterRule(_localctx, 0, RULE_program);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(42);
			packageDefinitionList();
			setState(43);
			match(EOF);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class PackageDefinitionListContext extends ParserRuleContext {
		public List<PackageDefinitionContext> packageDefinition() {
			return getRuleContexts(PackageDefinitionContext.class);
		}
		public PackageDefinitionContext packageDefinition(int i) {
			return getRuleContext(PackageDefinitionContext.class,i);
		}
		public PackageDefinitionListContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_packageDefinitionList; }
	}

	public final PackageDefinitionListContext packageDefinitionList() throws RecognitionException {
		PackageDefinitionListContext _localctx = new PackageDefinitionListContext(_ctx, getState());
		enterRule(_localctx, 2, RULE_packageDefinitionList);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(48);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Package) {
				{
				{
				setState(45);
				packageDefinition();
				}
				}
				setState(50);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class PackageDefinitionContext extends ParserRuleContext {
		public TerminalNode Package() { return getToken(crimsonParser.Package, 0); }
		public TerminalNode Identifier() { return getToken(crimsonParser.Identifier, 0); }
		public PackageDependencyListContext packageDependencyList() {
			return getRuleContext(PackageDependencyListContext.class,0);
		}
		public PackageBodyContext packageBody() {
			return getRuleContext(PackageBodyContext.class,0);
		}
		public PackageDefinitionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_packageDefinition; }
	}

	public final PackageDefinitionContext packageDefinition() throws RecognitionException {
		PackageDefinitionContext _localctx = new PackageDefinitionContext(_ctx, getState());
		enterRule(_localctx, 4, RULE_packageDefinition);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(51);
			match(Package);
			setState(52);
			match(Identifier);
			setState(53);
			packageDependencyList();
			setState(54);
			packageBody();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class PackageDependencyListContext extends ParserRuleContext {
		public TerminalNode OpenBracket() { return getToken(crimsonParser.OpenBracket, 0); }
		public TerminalNode CloseBracket() { return getToken(crimsonParser.CloseBracket, 0); }
		public List<PackageDependencyContext> packageDependency() {
			return getRuleContexts(PackageDependencyContext.class);
		}
		public PackageDependencyContext packageDependency(int i) {
			return getRuleContext(PackageDependencyContext.class,i);
		}
		public List<TerminalNode> Comma() { return getTokens(crimsonParser.Comma); }
		public TerminalNode Comma(int i) {
			return getToken(crimsonParser.Comma, i);
		}
		public PackageDependencyListContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_packageDependencyList; }
	}

	public final PackageDependencyListContext packageDependencyList() throws RecognitionException {
		PackageDependencyListContext _localctx = new PackageDependencyListContext(_ctx, getState());
		enterRule(_localctx, 6, RULE_packageDependencyList);
		int _la;
		try {
			setState(69);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,2,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(56);
				match(OpenBracket);
				setState(57);
				match(CloseBracket);
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(58);
				match(OpenBracket);
				setState(59);
				packageDependency();
				setState(64);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==Comma) {
					{
					{
					setState(60);
					match(Comma);
					setState(61);
					packageDependency();
					}
					}
					setState(66);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(67);
				match(CloseBracket);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class PackageDependencyContext extends ParserRuleContext {
		public List<TerminalNode> Identifier() { return getTokens(crimsonParser.Identifier); }
		public TerminalNode Identifier(int i) {
			return getToken(crimsonParser.Identifier, i);
		}
		public TerminalNode OpenBracket() { return getToken(crimsonParser.OpenBracket, 0); }
		public TerminalNode CloseBracket() { return getToken(crimsonParser.CloseBracket, 0); }
		public PackageDependencyContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_packageDependency; }
	}

	public final PackageDependencyContext packageDependency() throws RecognitionException {
		PackageDependencyContext _localctx = new PackageDependencyContext(_ctx, getState());
		enterRule(_localctx, 8, RULE_packageDependency);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(71);
			match(Identifier);
			setState(72);
			match(OpenBracket);
			setState(73);
			match(Identifier);
			setState(74);
			match(CloseBracket);
			setState(75);
			match(Identifier);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class PackageBodyContext extends ParserRuleContext {
		public TerminalNode OpenBrace() { return getToken(crimsonParser.OpenBrace, 0); }
		public TerminalNode CloseBrace() { return getToken(crimsonParser.CloseBrace, 0); }
		public List<TopLevelStatementContext> topLevelStatement() {
			return getRuleContexts(TopLevelStatementContext.class);
		}
		public TopLevelStatementContext topLevelStatement(int i) {
			return getRuleContext(TopLevelStatementContext.class,i);
		}
		public PackageBodyContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_packageBody; }
	}

	public final PackageBodyContext packageBody() throws RecognitionException {
		PackageBodyContext _localctx = new PackageBodyContext(_ctx, getState());
		enterRule(_localctx, 10, RULE_packageBody);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(77);
			match(OpenBrace);
			setState(81);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Function || _la==Global) {
				{
				{
				setState(78);
				topLevelStatement();
				}
				}
				setState(83);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(84);
			match(CloseBrace);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class TopLevelStatementContext extends ParserRuleContext {
		public GlobalVariableDeclarationContext globalVariableDeclaration() {
			return getRuleContext(GlobalVariableDeclarationContext.class,0);
		}
		public FunctionDeclarationContext functionDeclaration() {
			return getRuleContext(FunctionDeclarationContext.class,0);
		}
		public TopLevelStatementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_topLevelStatement; }
	}

	public final TopLevelStatementContext topLevelStatement() throws RecognitionException {
		TopLevelStatementContext _localctx = new TopLevelStatementContext(_ctx, getState());
		enterRule(_localctx, 12, RULE_topLevelStatement);
		try {
			setState(88);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case Global:
				enterOuterAlt(_localctx, 1);
				{
				setState(86);
				globalVariableDeclaration();
				}
				break;
			case Function:
				enterOuterAlt(_localctx, 2);
				{
				setState(87);
				functionDeclaration();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class GlobalVariableDeclarationContext extends ParserRuleContext {
		public TerminalNode Global() { return getToken(crimsonParser.Global, 0); }
		public InternalVariableDeclarationContext internalVariableDeclaration() {
			return getRuleContext(InternalVariableDeclarationContext.class,0);
		}
		public GlobalVariableDeclarationContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_globalVariableDeclaration; }
	}

	public final GlobalVariableDeclarationContext globalVariableDeclaration() throws RecognitionException {
		GlobalVariableDeclarationContext _localctx = new GlobalVariableDeclarationContext(_ctx, getState());
		enterRule(_localctx, 14, RULE_globalVariableDeclaration);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(90);
			match(Global);
			setState(91);
			internalVariableDeclaration();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class FunctionDeclarationContext extends ParserRuleContext {
		public TerminalNode Function() { return getToken(crimsonParser.Function, 0); }
		public TerminalNode Identifier() { return getToken(crimsonParser.Identifier, 0); }
		public FunctionReturnTypeContext functionReturnType() {
			return getRuleContext(FunctionReturnTypeContext.class,0);
		}
		public ParameterListContext parameterList() {
			return getRuleContext(ParameterListContext.class,0);
		}
		public FunctionBodyContext functionBody() {
			return getRuleContext(FunctionBodyContext.class,0);
		}
		public FunctionDeclarationContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_functionDeclaration; }
	}

	public final FunctionDeclarationContext functionDeclaration() throws RecognitionException {
		FunctionDeclarationContext _localctx = new FunctionDeclarationContext(_ctx, getState());
		enterRule(_localctx, 16, RULE_functionDeclaration);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(93);
			match(Function);
			setState(94);
			match(Identifier);
			setState(95);
			functionReturnType();
			setState(96);
			parameterList();
			setState(97);
			functionBody();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class FunctionReturnTypeContext extends ParserRuleContext {
		public TerminalNode OpenSquare() { return getToken(crimsonParser.OpenSquare, 0); }
		public ParameterTypeContext parameterType() {
			return getRuleContext(ParameterTypeContext.class,0);
		}
		public TerminalNode CloseSquare() { return getToken(crimsonParser.CloseSquare, 0); }
		public FunctionReturnTypeContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_functionReturnType; }
	}

	public final FunctionReturnTypeContext functionReturnType() throws RecognitionException {
		FunctionReturnTypeContext _localctx = new FunctionReturnTypeContext(_ctx, getState());
		enterRule(_localctx, 18, RULE_functionReturnType);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(99);
			match(OpenSquare);
			setState(100);
			parameterType();
			setState(101);
			match(CloseSquare);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class FunctionBodyContext extends ParserRuleContext {
		public TerminalNode OpenBrace() { return getToken(crimsonParser.OpenBrace, 0); }
		public TerminalNode CloseBrace() { return getToken(crimsonParser.CloseBrace, 0); }
		public List<FunctionOnlyStatementContext> functionOnlyStatement() {
			return getRuleContexts(FunctionOnlyStatementContext.class);
		}
		public FunctionOnlyStatementContext functionOnlyStatement(int i) {
			return getRuleContext(FunctionOnlyStatementContext.class,i);
		}
		public FunctionBodyContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_functionBody; }
	}

	public final FunctionBodyContext functionBody() throws RecognitionException {
		FunctionBodyContext _localctx = new FunctionBodyContext(_ctx, getState());
		enterRule(_localctx, 20, RULE_functionBody);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(103);
			match(OpenBrace);
			setState(107);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Return) | (1L << Integer) | (1L << Boolean) | (1L << Identifier))) != 0)) {
				{
				{
				setState(104);
				functionOnlyStatement();
				}
				}
				setState(109);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(110);
			match(CloseBrace);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class FunctionOnlyStatementContext extends ParserRuleContext {
		public InternalVariableDeclarationContext internalVariableDeclaration() {
			return getRuleContext(InternalVariableDeclarationContext.class,0);
		}
		public FunctionReturnContext functionReturn() {
			return getRuleContext(FunctionReturnContext.class,0);
		}
		public FunctionCallContext functionCall() {
			return getRuleContext(FunctionCallContext.class,0);
		}
		public TerminalNode SemiColon() { return getToken(crimsonParser.SemiColon, 0); }
		public FunctionOnlyStatementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_functionOnlyStatement; }
	}

	public final FunctionOnlyStatementContext functionOnlyStatement() throws RecognitionException {
		FunctionOnlyStatementContext _localctx = new FunctionOnlyStatementContext(_ctx, getState());
		enterRule(_localctx, 22, RULE_functionOnlyStatement);
		try {
			setState(117);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case Integer:
			case Boolean:
				enterOuterAlt(_localctx, 1);
				{
				setState(112);
				internalVariableDeclaration();
				}
				break;
			case Return:
				enterOuterAlt(_localctx, 2);
				{
				setState(113);
				functionReturn();
				}
				break;
			case Identifier:
				enterOuterAlt(_localctx, 3);
				{
				setState(114);
				functionCall();
				setState(115);
				match(SemiColon);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class InternalVariableDeclarationContext extends ParserRuleContext {
		public ParameterTypeContext parameterType() {
			return getRuleContext(ParameterTypeContext.class,0);
		}
		public TerminalNode Identifier() { return getToken(crimsonParser.Identifier, 0); }
		public TerminalNode SemiColon() { return getToken(crimsonParser.SemiColon, 0); }
		public TerminalNode Equals() { return getToken(crimsonParser.Equals, 0); }
		public ResolvableValueContext resolvableValue() {
			return getRuleContext(ResolvableValueContext.class,0);
		}
		public InternalVariableDeclarationContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_internalVariableDeclaration; }
	}

	public final InternalVariableDeclarationContext internalVariableDeclaration() throws RecognitionException {
		InternalVariableDeclarationContext _localctx = new InternalVariableDeclarationContext(_ctx, getState());
		enterRule(_localctx, 24, RULE_internalVariableDeclaration);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(119);
			parameterType();
			setState(120);
			match(Identifier);
			setState(123);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==Equals) {
				{
				setState(121);
				match(Equals);
				setState(122);
				resolvableValue();
				}
			}

			setState(125);
			match(SemiColon);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class AssignVariableContext extends ParserRuleContext {
		public TerminalNode Identifier() { return getToken(crimsonParser.Identifier, 0); }
		public TerminalNode Equals() { return getToken(crimsonParser.Equals, 0); }
		public ResolvableValueContext resolvableValue() {
			return getRuleContext(ResolvableValueContext.class,0);
		}
		public TerminalNode SemiColon() { return getToken(crimsonParser.SemiColon, 0); }
		public AssignVariableContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_assignVariable; }
	}

	public final AssignVariableContext assignVariable() throws RecognitionException {
		AssignVariableContext _localctx = new AssignVariableContext(_ctx, getState());
		enterRule(_localctx, 26, RULE_assignVariable);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(127);
			match(Identifier);
			setState(128);
			match(Equals);
			setState(129);
			resolvableValue();
			setState(130);
			match(SemiColon);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class FunctionCallContext extends ParserRuleContext {
		public TerminalNode Identifier() { return getToken(crimsonParser.Identifier, 0); }
		public InputParametersContext inputParameters() {
			return getRuleContext(InputParametersContext.class,0);
		}
		public FunctionCallContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_functionCall; }
	}

	public final FunctionCallContext functionCall() throws RecognitionException {
		FunctionCallContext _localctx = new FunctionCallContext(_ctx, getState());
		enterRule(_localctx, 28, RULE_functionCall);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(132);
			match(Identifier);
			setState(133);
			inputParameters();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class InputParametersContext extends ParserRuleContext {
		public TerminalNode OpenBracket() { return getToken(crimsonParser.OpenBracket, 0); }
		public TerminalNode CloseBracket() { return getToken(crimsonParser.CloseBracket, 0); }
		public List<TerminalNode> Identifier() { return getTokens(crimsonParser.Identifier); }
		public TerminalNode Identifier(int i) {
			return getToken(crimsonParser.Identifier, i);
		}
		public List<TerminalNode> Comma() { return getTokens(crimsonParser.Comma); }
		public TerminalNode Comma(int i) {
			return getToken(crimsonParser.Comma, i);
		}
		public InputParametersContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_inputParameters; }
	}

	public final InputParametersContext inputParameters() throws RecognitionException {
		InputParametersContext _localctx = new InputParametersContext(_ctx, getState());
		enterRule(_localctx, 30, RULE_inputParameters);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(135);
			match(OpenBracket);
			setState(137);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==Identifier) {
				{
				setState(136);
				match(Identifier);
				}
			}

			setState(143);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Comma) {
				{
				{
				setState(139);
				match(Comma);
				setState(140);
				match(Identifier);
				}
				}
				setState(145);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(146);
			match(CloseBracket);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class FunctionReturnContext extends ParserRuleContext {
		public TerminalNode Return() { return getToken(crimsonParser.Return, 0); }
		public ResolvableValueContext resolvableValue() {
			return getRuleContext(ResolvableValueContext.class,0);
		}
		public TerminalNode SemiColon() { return getToken(crimsonParser.SemiColon, 0); }
		public FunctionReturnContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_functionReturn; }
	}

	public final FunctionReturnContext functionReturn() throws RecognitionException {
		FunctionReturnContext _localctx = new FunctionReturnContext(_ctx, getState());
		enterRule(_localctx, 32, RULE_functionReturn);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(148);
			match(Return);
			setState(149);
			resolvableValue();
			setState(150);
			match(SemiColon);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ResolvableValueContext extends ParserRuleContext {
		public TerminalNode Value() { return getToken(crimsonParser.Value, 0); }
		public FunctionCallContext functionCall() {
			return getRuleContext(FunctionCallContext.class,0);
		}
		public ResolvableValueContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_resolvableValue; }
	}

	public final ResolvableValueContext resolvableValue() throws RecognitionException {
		ResolvableValueContext _localctx = new ResolvableValueContext(_ctx, getState());
		enterRule(_localctx, 34, RULE_resolvableValue);
		try {
			setState(154);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case Value:
				enterOuterAlt(_localctx, 1);
				{
				setState(152);
				match(Value);
				}
				break;
			case Identifier:
				enterOuterAlt(_localctx, 2);
				{
				setState(153);
				functionCall();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ParameterListContext extends ParserRuleContext {
		public TerminalNode OpenBracket() { return getToken(crimsonParser.OpenBracket, 0); }
		public TerminalNode CloseBracket() { return getToken(crimsonParser.CloseBracket, 0); }
		public List<ParameterContext> parameter() {
			return getRuleContexts(ParameterContext.class);
		}
		public ParameterContext parameter(int i) {
			return getRuleContext(ParameterContext.class,i);
		}
		public List<TerminalNode> Comma() { return getTokens(crimsonParser.Comma); }
		public TerminalNode Comma(int i) {
			return getToken(crimsonParser.Comma, i);
		}
		public ParameterListContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_parameterList; }
	}

	public final ParameterListContext parameterList() throws RecognitionException {
		ParameterListContext _localctx = new ParameterListContext(_ctx, getState());
		enterRule(_localctx, 36, RULE_parameterList);
		int _la;
		try {
			setState(169);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,12,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(156);
				match(OpenBracket);
				setState(157);
				match(CloseBracket);
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(158);
				match(OpenBracket);
				setState(159);
				parameter();
				setState(164);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==Comma) {
					{
					{
					setState(160);
					match(Comma);
					setState(161);
					parameter();
					}
					}
					setState(166);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(167);
				match(CloseBracket);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ParameterContext extends ParserRuleContext {
		public ParameterTypeContext parameterType() {
			return getRuleContext(ParameterTypeContext.class,0);
		}
		public TerminalNode Identifier() { return getToken(crimsonParser.Identifier, 0); }
		public ParameterContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_parameter; }
	}

	public final ParameterContext parameter() throws RecognitionException {
		ParameterContext _localctx = new ParameterContext(_ctx, getState());
		enterRule(_localctx, 38, RULE_parameter);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(171);
			parameterType();
			setState(172);
			match(Identifier);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ParameterTypeContext extends ParserRuleContext {
		public TerminalNode Integer() { return getToken(crimsonParser.Integer, 0); }
		public TerminalNode Boolean() { return getToken(crimsonParser.Boolean, 0); }
		public ParameterTypeContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_parameterType; }
	}

	public final ParameterTypeContext parameterType() throws RecognitionException {
		ParameterTypeContext _localctx = new ParameterTypeContext(_ctx, getState());
		enterRule(_localctx, 40, RULE_parameterType);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(174);
			_la = _input.LA(1);
			if ( !(_la==Integer || _la==Boolean) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3\24\u00b3\4\2\t\2"+
		"\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13"+
		"\t\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22"+
		"\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\3\2\3\2\3\2\3\3\7\3\61\n\3\f"+
		"\3\16\3\64\13\3\3\4\3\4\3\4\3\4\3\4\3\5\3\5\3\5\3\5\3\5\3\5\7\5A\n\5\f"+
		"\5\16\5D\13\5\3\5\3\5\5\5H\n\5\3\6\3\6\3\6\3\6\3\6\3\6\3\7\3\7\7\7R\n"+
		"\7\f\7\16\7U\13\7\3\7\3\7\3\b\3\b\5\b[\n\b\3\t\3\t\3\t\3\n\3\n\3\n\3\n"+
		"\3\n\3\n\3\13\3\13\3\13\3\13\3\f\3\f\7\fl\n\f\f\f\16\fo\13\f\3\f\3\f\3"+
		"\r\3\r\3\r\3\r\3\r\5\rx\n\r\3\16\3\16\3\16\3\16\5\16~\n\16\3\16\3\16\3"+
		"\17\3\17\3\17\3\17\3\17\3\20\3\20\3\20\3\21\3\21\5\21\u008c\n\21\3\21"+
		"\3\21\7\21\u0090\n\21\f\21\16\21\u0093\13\21\3\21\3\21\3\22\3\22\3\22"+
		"\3\22\3\23\3\23\5\23\u009d\n\23\3\24\3\24\3\24\3\24\3\24\3\24\7\24\u00a5"+
		"\n\24\f\24\16\24\u00a8\13\24\3\24\3\24\5\24\u00ac\n\24\3\25\3\25\3\25"+
		"\3\26\3\26\3\26\2\2\27\2\4\6\b\n\f\16\20\22\24\26\30\32\34\36 \"$&(*\2"+
		"\3\3\2\7\b\2\u00ab\2,\3\2\2\2\4\62\3\2\2\2\6\65\3\2\2\2\bG\3\2\2\2\nI"+
		"\3\2\2\2\fO\3\2\2\2\16Z\3\2\2\2\20\\\3\2\2\2\22_\3\2\2\2\24e\3\2\2\2\26"+
		"i\3\2\2\2\30w\3\2\2\2\32y\3\2\2\2\34\u0081\3\2\2\2\36\u0086\3\2\2\2 \u0089"+
		"\3\2\2\2\"\u0096\3\2\2\2$\u009c\3\2\2\2&\u00ab\3\2\2\2(\u00ad\3\2\2\2"+
		"*\u00b0\3\2\2\2,-\5\4\3\2-.\7\2\2\3.\3\3\2\2\2/\61\5\6\4\2\60/\3\2\2\2"+
		"\61\64\3\2\2\2\62\60\3\2\2\2\62\63\3\2\2\2\63\5\3\2\2\2\64\62\3\2\2\2"+
		"\65\66\7\3\2\2\66\67\7\23\2\2\678\5\b\5\289\5\f\7\29\7\3\2\2\2:;\7\n\2"+
		"\2;H\7\13\2\2<=\7\n\2\2=B\5\n\6\2>?\7\20\2\2?A\5\n\6\2@>\3\2\2\2AD\3\2"+
		"\2\2B@\3\2\2\2BC\3\2\2\2CE\3\2\2\2DB\3\2\2\2EF\7\13\2\2FH\3\2\2\2G:\3"+
		"\2\2\2G<\3\2\2\2H\t\3\2\2\2IJ\7\23\2\2JK\7\n\2\2KL\7\23\2\2LM\7\13\2\2"+
		"MN\7\23\2\2N\13\3\2\2\2OS\7\16\2\2PR\5\16\b\2QP\3\2\2\2RU\3\2\2\2SQ\3"+
		"\2\2\2ST\3\2\2\2TV\3\2\2\2US\3\2\2\2VW\7\17\2\2W\r\3\2\2\2X[\5\20\t\2"+
		"Y[\5\22\n\2ZX\3\2\2\2ZY\3\2\2\2[\17\3\2\2\2\\]\7\5\2\2]^\5\32\16\2^\21"+
		"\3\2\2\2_`\7\4\2\2`a\7\23\2\2ab\5\24\13\2bc\5&\24\2cd\5\26\f\2d\23\3\2"+
		"\2\2ef\7\f\2\2fg\5*\26\2gh\7\r\2\2h\25\3\2\2\2im\7\16\2\2jl\5\30\r\2k"+
		"j\3\2\2\2lo\3\2\2\2mk\3\2\2\2mn\3\2\2\2np\3\2\2\2om\3\2\2\2pq\7\17\2\2"+
		"q\27\3\2\2\2rx\5\32\16\2sx\5\"\22\2tu\5\36\20\2uv\7\22\2\2vx\3\2\2\2w"+
		"r\3\2\2\2ws\3\2\2\2wt\3\2\2\2x\31\3\2\2\2yz\5*\26\2z}\7\23\2\2{|\7\t\2"+
		"\2|~\5$\23\2}{\3\2\2\2}~\3\2\2\2~\177\3\2\2\2\177\u0080\7\22\2\2\u0080"+
		"\33\3\2\2\2\u0081\u0082\7\23\2\2\u0082\u0083\7\t\2\2\u0083\u0084\5$\23"+
		"\2\u0084\u0085\7\22\2\2\u0085\35\3\2\2\2\u0086\u0087\7\23\2\2\u0087\u0088"+
		"\5 \21\2\u0088\37\3\2\2\2\u0089\u008b\7\n\2\2\u008a\u008c\7\23\2\2\u008b"+
		"\u008a\3\2\2\2\u008b\u008c\3\2\2\2\u008c\u0091\3\2\2\2\u008d\u008e\7\20"+
		"\2\2\u008e\u0090\7\23\2\2\u008f\u008d\3\2\2\2\u0090\u0093\3\2\2\2\u0091"+
		"\u008f\3\2\2\2\u0091\u0092\3\2\2\2\u0092\u0094\3\2\2\2\u0093\u0091\3\2"+
		"\2\2\u0094\u0095\7\13\2\2\u0095!\3\2\2\2\u0096\u0097\7\6\2\2\u0097\u0098"+
		"\5$\23\2\u0098\u0099\7\22\2\2\u0099#\3\2\2\2\u009a\u009d\7\24\2\2\u009b"+
		"\u009d\5\36\20\2\u009c\u009a\3\2\2\2\u009c\u009b\3\2\2\2\u009d%\3\2\2"+
		"\2\u009e\u009f\7\n\2\2\u009f\u00ac\7\13\2\2\u00a0\u00a1\7\n\2\2\u00a1"+
		"\u00a6\5(\25\2\u00a2\u00a3\7\20\2\2\u00a3\u00a5\5(\25\2\u00a4\u00a2\3"+
		"\2\2\2\u00a5\u00a8\3\2\2\2\u00a6\u00a4\3\2\2\2\u00a6\u00a7\3\2\2\2\u00a7"+
		"\u00a9\3\2\2\2\u00a8\u00a6\3\2\2\2\u00a9\u00aa\7\13\2\2\u00aa\u00ac\3"+
		"\2\2\2\u00ab\u009e\3\2\2\2\u00ab\u00a0\3\2\2\2\u00ac\'\3\2\2\2\u00ad\u00ae"+
		"\5*\26\2\u00ae\u00af\7\23\2\2\u00af)\3\2\2\2\u00b0\u00b1\t\2\2\2\u00b1"+
		"+\3\2\2\2\17\62BGSZmw}\u008b\u0091\u009c\u00a6\u00ab";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}