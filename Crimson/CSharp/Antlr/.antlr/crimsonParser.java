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
		Package=1, Function=2, Global=3, Return=4, Structure=5, If=6, Else=7, 
		Elif=8, Integer=9, Boolean=10, Null=11, BooleanValue=12, Comparator=13, 
		Equals=14, OpenBracket=15, CloseBracket=16, OpenSquare=17, CloseSquare=18, 
		OpenBrace=19, CloseBrace=20, Comma=21, Dot=22, SemiColon=23, Underscore=24, 
		Number=25, Identifier=26;
	public static final int
		RULE_program = 0, RULE_packageDefinitionList = 1, RULE_packageDefinition = 2, 
		RULE_packageDependencyList = 3, RULE_packageDependency = 4, RULE_packageBody = 5, 
		RULE_topLevelStatement = 6, RULE_globalVariableDeclaration = 7, RULE_functionDeclaration = 8, 
		RULE_functionBody = 9, RULE_functionOnlyStatement = 10, RULE_internalVariableDeclaration = 11, 
		RULE_assignVariable = 12, RULE_ifBlock = 13, RULE_condition = 14, RULE_elifBlock = 15, 
		RULE_elseBlock = 16, RULE_functionCall = 17, RULE_inputParameters = 18, 
		RULE_functionReturn = 19, RULE_resolvableValue = 20, RULE_parameterList = 21, 
		RULE_parameter = 22, RULE_structureDeclaration = 23, RULE_structureBody = 24, 
		RULE_type = 25, RULE_array = 26;
	private static String[] makeRuleNames() {
		return new String[] {
			"program", "packageDefinitionList", "packageDefinition", "packageDependencyList", 
			"packageDependency", "packageBody", "topLevelStatement", "globalVariableDeclaration", 
			"functionDeclaration", "functionBody", "functionOnlyStatement", "internalVariableDeclaration", 
			"assignVariable", "ifBlock", "condition", "elifBlock", "elseBlock", "functionCall", 
			"inputParameters", "functionReturn", "resolvableValue", "parameterList", 
			"parameter", "structureDeclaration", "structureBody", "type", "array"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "'package'", "'function'", "'global'", "'return'", "'structure'", 
			"'if'", "'else'", "'elif'", "'int'", "'bool'", "'null'", null, null, 
			"'='", "'('", "')'", "'['", "']'", "'{'", "'}'", "','", "'.'", "';'", 
			"'_'"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, "Package", "Function", "Global", "Return", "Structure", "If", "Else", 
			"Elif", "Integer", "Boolean", "Null", "BooleanValue", "Comparator", "Equals", 
			"OpenBracket", "CloseBracket", "OpenSquare", "CloseSquare", "OpenBrace", 
			"CloseBrace", "Comma", "Dot", "SemiColon", "Underscore", "Number", "Identifier"
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
			setState(54);
			packageDefinitionList();
			setState(55);
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
			setState(60);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Package) {
				{
				{
				setState(57);
				packageDefinition();
				}
				}
				setState(62);
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
			setState(63);
			match(Package);
			setState(64);
			match(Identifier);
			setState(65);
			packageDependencyList();
			setState(66);
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
			setState(81);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,2,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(68);
				match(OpenBracket);
				setState(69);
				match(CloseBracket);
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(70);
				match(OpenBracket);
				setState(71);
				packageDependency();
				setState(76);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==Comma) {
					{
					{
					setState(72);
					match(Comma);
					setState(73);
					packageDependency();
					}
					}
					setState(78);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(79);
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
			setState(83);
			match(Identifier);
			setState(84);
			match(OpenBracket);
			setState(85);
			match(Identifier);
			setState(86);
			match(CloseBracket);
			setState(87);
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
			setState(89);
			match(OpenBrace);
			setState(93);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Function) | (1L << Global) | (1L << Structure))) != 0)) {
				{
				{
				setState(90);
				topLevelStatement();
				}
				}
				setState(95);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(96);
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
		public StructureDeclarationContext structureDeclaration() {
			return getRuleContext(StructureDeclarationContext.class,0);
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
			setState(101);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case Global:
				enterOuterAlt(_localctx, 1);
				{
				setState(98);
				globalVariableDeclaration();
				}
				break;
			case Function:
				enterOuterAlt(_localctx, 2);
				{
				setState(99);
				functionDeclaration();
				}
				break;
			case Structure:
				enterOuterAlt(_localctx, 3);
				{
				setState(100);
				structureDeclaration();
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
			setState(103);
			match(Global);
			setState(104);
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
		public TypeContext type() {
			return getRuleContext(TypeContext.class,0);
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
			setState(106);
			match(Function);
			setState(107);
			match(Identifier);
			setState(108);
			type();
			setState(109);
			parameterList();
			setState(110);
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
		enterRule(_localctx, 18, RULE_functionBody);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(112);
			match(OpenBrace);
			setState(116);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Return) | (1L << If) | (1L << Integer) | (1L << Boolean) | (1L << Null) | (1L << OpenSquare) | (1L << Identifier))) != 0)) {
				{
				{
				setState(113);
				functionOnlyStatement();
				}
				}
				setState(118);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(119);
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
		public IfBlockContext ifBlock() {
			return getRuleContext(IfBlockContext.class,0);
		}
		public FunctionOnlyStatementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_functionOnlyStatement; }
	}

	public final FunctionOnlyStatementContext functionOnlyStatement() throws RecognitionException {
		FunctionOnlyStatementContext _localctx = new FunctionOnlyStatementContext(_ctx, getState());
		enterRule(_localctx, 20, RULE_functionOnlyStatement);
		try {
			setState(127);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,6,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(121);
				internalVariableDeclaration();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(122);
				functionReturn();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(123);
				functionCall();
				setState(124);
				match(SemiColon);
				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(126);
				ifBlock();
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

	public static class InternalVariableDeclarationContext extends ParserRuleContext {
		public TypeContext type() {
			return getRuleContext(TypeContext.class,0);
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
		enterRule(_localctx, 22, RULE_internalVariableDeclaration);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(129);
			type();
			setState(130);
			match(Identifier);
			setState(133);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==Equals) {
				{
				setState(131);
				match(Equals);
				setState(132);
				resolvableValue();
				}
			}

			setState(135);
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
		enterRule(_localctx, 24, RULE_assignVariable);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(137);
			match(Identifier);
			setState(138);
			match(Equals);
			setState(139);
			resolvableValue();
			setState(140);
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

	public static class IfBlockContext extends ParserRuleContext {
		public TerminalNode If() { return getToken(crimsonParser.If, 0); }
		public ConditionContext condition() {
			return getRuleContext(ConditionContext.class,0);
		}
		public FunctionBodyContext functionBody() {
			return getRuleContext(FunctionBodyContext.class,0);
		}
		public ElseBlockContext elseBlock() {
			return getRuleContext(ElseBlockContext.class,0);
		}
		public ElifBlockContext elifBlock() {
			return getRuleContext(ElifBlockContext.class,0);
		}
		public IfBlockContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_ifBlock; }
	}

	public final IfBlockContext ifBlock() throws RecognitionException {
		IfBlockContext _localctx = new IfBlockContext(_ctx, getState());
		enterRule(_localctx, 26, RULE_ifBlock);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(142);
			match(If);
			setState(143);
			condition();
			setState(144);
			functionBody();
			setState(147);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case Else:
				{
				setState(145);
				elseBlock();
				}
				break;
			case Elif:
				{
				setState(146);
				elifBlock();
				}
				break;
			case Return:
			case If:
			case Integer:
			case Boolean:
			case Null:
			case OpenSquare:
			case CloseBrace:
			case Identifier:
				break;
			default:
				break;
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

	public static class ConditionContext extends ParserRuleContext {
		public TerminalNode OpenBracket() { return getToken(crimsonParser.OpenBracket, 0); }
		public TerminalNode BooleanValue() { return getToken(crimsonParser.BooleanValue, 0); }
		public TerminalNode CloseBracket() { return getToken(crimsonParser.CloseBracket, 0); }
		public List<ResolvableValueContext> resolvableValue() {
			return getRuleContexts(ResolvableValueContext.class);
		}
		public ResolvableValueContext resolvableValue(int i) {
			return getRuleContext(ResolvableValueContext.class,i);
		}
		public TerminalNode Comparator() { return getToken(crimsonParser.Comparator, 0); }
		public ConditionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_condition; }
	}

	public final ConditionContext condition() throws RecognitionException {
		ConditionContext _localctx = new ConditionContext(_ctx, getState());
		enterRule(_localctx, 28, RULE_condition);
		try {
			setState(158);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,9,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(149);
				match(OpenBracket);
				setState(150);
				match(BooleanValue);
				setState(151);
				match(CloseBracket);
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(152);
				match(OpenBracket);
				setState(153);
				resolvableValue();
				setState(154);
				match(Comparator);
				setState(155);
				resolvableValue();
				setState(156);
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

	public static class ElifBlockContext extends ParserRuleContext {
		public TerminalNode Elif() { return getToken(crimsonParser.Elif, 0); }
		public ConditionContext condition() {
			return getRuleContext(ConditionContext.class,0);
		}
		public FunctionBodyContext functionBody() {
			return getRuleContext(FunctionBodyContext.class,0);
		}
		public ElseBlockContext elseBlock() {
			return getRuleContext(ElseBlockContext.class,0);
		}
		public ElifBlockContext elifBlock() {
			return getRuleContext(ElifBlockContext.class,0);
		}
		public ElifBlockContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_elifBlock; }
	}

	public final ElifBlockContext elifBlock() throws RecognitionException {
		ElifBlockContext _localctx = new ElifBlockContext(_ctx, getState());
		enterRule(_localctx, 30, RULE_elifBlock);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(160);
			match(Elif);
			setState(161);
			condition();
			setState(162);
			functionBody();
			setState(165);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case Else:
				{
				setState(163);
				elseBlock();
				}
				break;
			case Elif:
				{
				setState(164);
				elifBlock();
				}
				break;
			case Return:
			case If:
			case Integer:
			case Boolean:
			case Null:
			case OpenSquare:
			case CloseBrace:
			case Identifier:
				break;
			default:
				break;
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

	public static class ElseBlockContext extends ParserRuleContext {
		public TerminalNode Else() { return getToken(crimsonParser.Else, 0); }
		public FunctionBodyContext functionBody() {
			return getRuleContext(FunctionBodyContext.class,0);
		}
		public ElseBlockContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_elseBlock; }
	}

	public final ElseBlockContext elseBlock() throws RecognitionException {
		ElseBlockContext _localctx = new ElseBlockContext(_ctx, getState());
		enterRule(_localctx, 32, RULE_elseBlock);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(167);
			match(Else);
			setState(168);
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
		enterRule(_localctx, 34, RULE_functionCall);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(170);
			match(Identifier);
			setState(171);
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
		enterRule(_localctx, 36, RULE_inputParameters);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(173);
			match(OpenBracket);
			setState(175);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==Identifier) {
				{
				setState(174);
				match(Identifier);
				}
			}

			setState(181);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Comma) {
				{
				{
				setState(177);
				match(Comma);
				setState(178);
				match(Identifier);
				}
				}
				setState(183);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(184);
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
		enterRule(_localctx, 38, RULE_functionReturn);
		try {
			setState(192);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,13,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(186);
				match(Return);
				setState(187);
				resolvableValue();
				setState(188);
				match(SemiColon);
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(190);
				match(Return);
				setState(191);
				match(SemiColon);
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

	public static class ResolvableValueContext extends ParserRuleContext {
		public TerminalNode Identifier() { return getToken(crimsonParser.Identifier, 0); }
		public TerminalNode Number() { return getToken(crimsonParser.Number, 0); }
		public FunctionCallContext functionCall() {
			return getRuleContext(FunctionCallContext.class,0);
		}
		public TerminalNode Null() { return getToken(crimsonParser.Null, 0); }
		public TerminalNode BooleanValue() { return getToken(crimsonParser.BooleanValue, 0); }
		public ResolvableValueContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_resolvableValue; }
	}

	public final ResolvableValueContext resolvableValue() throws RecognitionException {
		ResolvableValueContext _localctx = new ResolvableValueContext(_ctx, getState());
		enterRule(_localctx, 40, RULE_resolvableValue);
		try {
			setState(199);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,14,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(194);
				match(Identifier);
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(195);
				match(Number);
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(196);
				functionCall();
				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(197);
				match(Null);
				}
				break;
			case 5:
				enterOuterAlt(_localctx, 5);
				{
				setState(198);
				match(BooleanValue);
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
		enterRule(_localctx, 42, RULE_parameterList);
		int _la;
		try {
			setState(214);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,16,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(201);
				match(OpenBracket);
				setState(202);
				match(CloseBracket);
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(203);
				match(OpenBracket);
				setState(204);
				parameter();
				setState(209);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==Comma) {
					{
					{
					setState(205);
					match(Comma);
					setState(206);
					parameter();
					}
					}
					setState(211);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(212);
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
		public TypeContext type() {
			return getRuleContext(TypeContext.class,0);
		}
		public TerminalNode Identifier() { return getToken(crimsonParser.Identifier, 0); }
		public ParameterContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_parameter; }
	}

	public final ParameterContext parameter() throws RecognitionException {
		ParameterContext _localctx = new ParameterContext(_ctx, getState());
		enterRule(_localctx, 44, RULE_parameter);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(216);
			type();
			setState(217);
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

	public static class StructureDeclarationContext extends ParserRuleContext {
		public TerminalNode Structure() { return getToken(crimsonParser.Structure, 0); }
		public TerminalNode Identifier() { return getToken(crimsonParser.Identifier, 0); }
		public StructureBodyContext structureBody() {
			return getRuleContext(StructureBodyContext.class,0);
		}
		public StructureDeclarationContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_structureDeclaration; }
	}

	public final StructureDeclarationContext structureDeclaration() throws RecognitionException {
		StructureDeclarationContext _localctx = new StructureDeclarationContext(_ctx, getState());
		enterRule(_localctx, 46, RULE_structureDeclaration);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(219);
			match(Structure);
			setState(220);
			match(Identifier);
			setState(221);
			structureBody();
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

	public static class StructureBodyContext extends ParserRuleContext {
		public TerminalNode OpenBrace() { return getToken(crimsonParser.OpenBrace, 0); }
		public TerminalNode CloseBrace() { return getToken(crimsonParser.CloseBrace, 0); }
		public List<InternalVariableDeclarationContext> internalVariableDeclaration() {
			return getRuleContexts(InternalVariableDeclarationContext.class);
		}
		public InternalVariableDeclarationContext internalVariableDeclaration(int i) {
			return getRuleContext(InternalVariableDeclarationContext.class,i);
		}
		public StructureBodyContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_structureBody; }
	}

	public final StructureBodyContext structureBody() throws RecognitionException {
		StructureBodyContext _localctx = new StructureBodyContext(_ctx, getState());
		enterRule(_localctx, 48, RULE_structureBody);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(223);
			match(OpenBrace);
			setState(227);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Integer) | (1L << Boolean) | (1L << Null) | (1L << OpenSquare) | (1L << Identifier))) != 0)) {
				{
				{
				setState(224);
				internalVariableDeclaration();
				}
				}
				setState(229);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(230);
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

	public static class TypeContext extends ParserRuleContext {
		public TerminalNode Integer() { return getToken(crimsonParser.Integer, 0); }
		public TerminalNode Boolean() { return getToken(crimsonParser.Boolean, 0); }
		public TerminalNode Identifier() { return getToken(crimsonParser.Identifier, 0); }
		public ArrayContext array() {
			return getRuleContext(ArrayContext.class,0);
		}
		public TerminalNode Null() { return getToken(crimsonParser.Null, 0); }
		public TypeContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_type; }
	}

	public final TypeContext type() throws RecognitionException {
		TypeContext _localctx = new TypeContext(_ctx, getState());
		enterRule(_localctx, 50, RULE_type);
		try {
			setState(237);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case Integer:
				enterOuterAlt(_localctx, 1);
				{
				setState(232);
				match(Integer);
				}
				break;
			case Boolean:
				enterOuterAlt(_localctx, 2);
				{
				setState(233);
				match(Boolean);
				}
				break;
			case Identifier:
				enterOuterAlt(_localctx, 3);
				{
				setState(234);
				match(Identifier);
				}
				break;
			case OpenSquare:
				enterOuterAlt(_localctx, 4);
				{
				setState(235);
				array();
				}
				break;
			case Null:
				enterOuterAlt(_localctx, 5);
				{
				setState(236);
				match(Null);
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

	public static class ArrayContext extends ParserRuleContext {
		public TerminalNode OpenSquare() { return getToken(crimsonParser.OpenSquare, 0); }
		public TypeContext type() {
			return getRuleContext(TypeContext.class,0);
		}
		public TerminalNode CloseSquare() { return getToken(crimsonParser.CloseSquare, 0); }
		public ArrayContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_array; }
	}

	public final ArrayContext array() throws RecognitionException {
		ArrayContext _localctx = new ArrayContext(_ctx, getState());
		enterRule(_localctx, 52, RULE_array);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(239);
			match(OpenSquare);
			setState(240);
			type();
			setState(241);
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

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3\34\u00f6\4\2\t\2"+
		"\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13"+
		"\t\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22"+
		"\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\4\31\t\31"+
		"\4\32\t\32\4\33\t\33\4\34\t\34\3\2\3\2\3\2\3\3\7\3=\n\3\f\3\16\3@\13\3"+
		"\3\4\3\4\3\4\3\4\3\4\3\5\3\5\3\5\3\5\3\5\3\5\7\5M\n\5\f\5\16\5P\13\5\3"+
		"\5\3\5\5\5T\n\5\3\6\3\6\3\6\3\6\3\6\3\6\3\7\3\7\7\7^\n\7\f\7\16\7a\13"+
		"\7\3\7\3\7\3\b\3\b\3\b\5\bh\n\b\3\t\3\t\3\t\3\n\3\n\3\n\3\n\3\n\3\n\3"+
		"\13\3\13\7\13u\n\13\f\13\16\13x\13\13\3\13\3\13\3\f\3\f\3\f\3\f\3\f\3"+
		"\f\5\f\u0082\n\f\3\r\3\r\3\r\3\r\5\r\u0088\n\r\3\r\3\r\3\16\3\16\3\16"+
		"\3\16\3\16\3\17\3\17\3\17\3\17\3\17\5\17\u0096\n\17\3\20\3\20\3\20\3\20"+
		"\3\20\3\20\3\20\3\20\3\20\5\20\u00a1\n\20\3\21\3\21\3\21\3\21\3\21\5\21"+
		"\u00a8\n\21\3\22\3\22\3\22\3\23\3\23\3\23\3\24\3\24\5\24\u00b2\n\24\3"+
		"\24\3\24\7\24\u00b6\n\24\f\24\16\24\u00b9\13\24\3\24\3\24\3\25\3\25\3"+
		"\25\3\25\3\25\3\25\5\25\u00c3\n\25\3\26\3\26\3\26\3\26\3\26\5\26\u00ca"+
		"\n\26\3\27\3\27\3\27\3\27\3\27\3\27\7\27\u00d2\n\27\f\27\16\27\u00d5\13"+
		"\27\3\27\3\27\5\27\u00d9\n\27\3\30\3\30\3\30\3\31\3\31\3\31\3\31\3\32"+
		"\3\32\7\32\u00e4\n\32\f\32\16\32\u00e7\13\32\3\32\3\32\3\33\3\33\3\33"+
		"\3\33\3\33\5\33\u00f0\n\33\3\34\3\34\3\34\3\34\3\34\2\2\35\2\4\6\b\n\f"+
		"\16\20\22\24\26\30\32\34\36 \"$&(*,.\60\62\64\66\2\2\2\u00f8\28\3\2\2"+
		"\2\4>\3\2\2\2\6A\3\2\2\2\bS\3\2\2\2\nU\3\2\2\2\f[\3\2\2\2\16g\3\2\2\2"+
		"\20i\3\2\2\2\22l\3\2\2\2\24r\3\2\2\2\26\u0081\3\2\2\2\30\u0083\3\2\2\2"+
		"\32\u008b\3\2\2\2\34\u0090\3\2\2\2\36\u00a0\3\2\2\2 \u00a2\3\2\2\2\"\u00a9"+
		"\3\2\2\2$\u00ac\3\2\2\2&\u00af\3\2\2\2(\u00c2\3\2\2\2*\u00c9\3\2\2\2,"+
		"\u00d8\3\2\2\2.\u00da\3\2\2\2\60\u00dd\3\2\2\2\62\u00e1\3\2\2\2\64\u00ef"+
		"\3\2\2\2\66\u00f1\3\2\2\289\5\4\3\29:\7\2\2\3:\3\3\2\2\2;=\5\6\4\2<;\3"+
		"\2\2\2=@\3\2\2\2><\3\2\2\2>?\3\2\2\2?\5\3\2\2\2@>\3\2\2\2AB\7\3\2\2BC"+
		"\7\34\2\2CD\5\b\5\2DE\5\f\7\2E\7\3\2\2\2FG\7\21\2\2GT\7\22\2\2HI\7\21"+
		"\2\2IN\5\n\6\2JK\7\27\2\2KM\5\n\6\2LJ\3\2\2\2MP\3\2\2\2NL\3\2\2\2NO\3"+
		"\2\2\2OQ\3\2\2\2PN\3\2\2\2QR\7\22\2\2RT\3\2\2\2SF\3\2\2\2SH\3\2\2\2T\t"+
		"\3\2\2\2UV\7\34\2\2VW\7\21\2\2WX\7\34\2\2XY\7\22\2\2YZ\7\34\2\2Z\13\3"+
		"\2\2\2[_\7\25\2\2\\^\5\16\b\2]\\\3\2\2\2^a\3\2\2\2_]\3\2\2\2_`\3\2\2\2"+
		"`b\3\2\2\2a_\3\2\2\2bc\7\26\2\2c\r\3\2\2\2dh\5\20\t\2eh\5\22\n\2fh\5\60"+
		"\31\2gd\3\2\2\2ge\3\2\2\2gf\3\2\2\2h\17\3\2\2\2ij\7\5\2\2jk\5\30\r\2k"+
		"\21\3\2\2\2lm\7\4\2\2mn\7\34\2\2no\5\64\33\2op\5,\27\2pq\5\24\13\2q\23"+
		"\3\2\2\2rv\7\25\2\2su\5\26\f\2ts\3\2\2\2ux\3\2\2\2vt\3\2\2\2vw\3\2\2\2"+
		"wy\3\2\2\2xv\3\2\2\2yz\7\26\2\2z\25\3\2\2\2{\u0082\5\30\r\2|\u0082\5("+
		"\25\2}~\5$\23\2~\177\7\31\2\2\177\u0082\3\2\2\2\u0080\u0082\5\34\17\2"+
		"\u0081{\3\2\2\2\u0081|\3\2\2\2\u0081}\3\2\2\2\u0081\u0080\3\2\2\2\u0082"+
		"\27\3\2\2\2\u0083\u0084\5\64\33\2\u0084\u0087\7\34\2\2\u0085\u0086\7\20"+
		"\2\2\u0086\u0088\5*\26\2\u0087\u0085\3\2\2\2\u0087\u0088\3\2\2\2\u0088"+
		"\u0089\3\2\2\2\u0089\u008a\7\31\2\2\u008a\31\3\2\2\2\u008b\u008c\7\34"+
		"\2\2\u008c\u008d\7\20\2\2\u008d\u008e\5*\26\2\u008e\u008f\7\31\2\2\u008f"+
		"\33\3\2\2\2\u0090\u0091\7\b\2\2\u0091\u0092\5\36\20\2\u0092\u0095\5\24"+
		"\13\2\u0093\u0096\5\"\22\2\u0094\u0096\5 \21\2\u0095\u0093\3\2\2\2\u0095"+
		"\u0094\3\2\2\2\u0095\u0096\3\2\2\2\u0096\35\3\2\2\2\u0097\u0098\7\21\2"+
		"\2\u0098\u0099\7\16\2\2\u0099\u00a1\7\22\2\2\u009a\u009b\7\21\2\2\u009b"+
		"\u009c\5*\26\2\u009c\u009d\7\17\2\2\u009d\u009e\5*\26\2\u009e\u009f\7"+
		"\22\2\2\u009f\u00a1\3\2\2\2\u00a0\u0097\3\2\2\2\u00a0\u009a\3\2\2\2\u00a1"+
		"\37\3\2\2\2\u00a2\u00a3\7\n\2\2\u00a3\u00a4\5\36\20\2\u00a4\u00a7\5\24"+
		"\13\2\u00a5\u00a8\5\"\22\2\u00a6\u00a8\5 \21\2\u00a7\u00a5\3\2\2\2\u00a7"+
		"\u00a6\3\2\2\2\u00a7\u00a8\3\2\2\2\u00a8!\3\2\2\2\u00a9\u00aa\7\t\2\2"+
		"\u00aa\u00ab\5\24\13\2\u00ab#\3\2\2\2\u00ac\u00ad\7\34\2\2\u00ad\u00ae"+
		"\5&\24\2\u00ae%\3\2\2\2\u00af\u00b1\7\21\2\2\u00b0\u00b2\7\34\2\2\u00b1"+
		"\u00b0\3\2\2\2\u00b1\u00b2\3\2\2\2\u00b2\u00b7\3\2\2\2\u00b3\u00b4\7\27"+
		"\2\2\u00b4\u00b6\7\34\2\2\u00b5\u00b3\3\2\2\2\u00b6\u00b9\3\2\2\2\u00b7"+
		"\u00b5\3\2\2\2\u00b7\u00b8\3\2\2\2\u00b8\u00ba\3\2\2\2\u00b9\u00b7\3\2"+
		"\2\2\u00ba\u00bb\7\22\2\2\u00bb\'\3\2\2\2\u00bc\u00bd\7\6\2\2\u00bd\u00be"+
		"\5*\26\2\u00be\u00bf\7\31\2\2\u00bf\u00c3\3\2\2\2\u00c0\u00c1\7\6\2\2"+
		"\u00c1\u00c3\7\31\2\2\u00c2\u00bc\3\2\2\2\u00c2\u00c0\3\2\2\2\u00c3)\3"+
		"\2\2\2\u00c4\u00ca\7\34\2\2\u00c5\u00ca\7\33\2\2\u00c6\u00ca\5$\23\2\u00c7"+
		"\u00ca\7\r\2\2\u00c8\u00ca\7\16\2\2\u00c9\u00c4\3\2\2\2\u00c9\u00c5\3"+
		"\2\2\2\u00c9\u00c6\3\2\2\2\u00c9\u00c7\3\2\2\2\u00c9\u00c8\3\2\2\2\u00ca"+
		"+\3\2\2\2\u00cb\u00cc\7\21\2\2\u00cc\u00d9\7\22\2\2\u00cd\u00ce\7\21\2"+
		"\2\u00ce\u00d3\5.\30\2\u00cf\u00d0\7\27\2\2\u00d0\u00d2\5.\30\2\u00d1"+
		"\u00cf\3\2\2\2\u00d2\u00d5\3\2\2\2\u00d3\u00d1\3\2\2\2\u00d3\u00d4\3\2"+
		"\2\2\u00d4\u00d6\3\2\2\2\u00d5\u00d3\3\2\2\2\u00d6\u00d7\7\22\2\2\u00d7"+
		"\u00d9\3\2\2\2\u00d8\u00cb\3\2\2\2\u00d8\u00cd\3\2\2\2\u00d9-\3\2\2\2"+
		"\u00da\u00db\5\64\33\2\u00db\u00dc\7\34\2\2\u00dc/\3\2\2\2\u00dd\u00de"+
		"\7\7\2\2\u00de\u00df\7\34\2\2\u00df\u00e0\5\62\32\2\u00e0\61\3\2\2\2\u00e1"+
		"\u00e5\7\25\2\2\u00e2\u00e4\5\30\r\2\u00e3\u00e2\3\2\2\2\u00e4\u00e7\3"+
		"\2\2\2\u00e5\u00e3\3\2\2\2\u00e5\u00e6\3\2\2\2\u00e6\u00e8\3\2\2\2\u00e7"+
		"\u00e5\3\2\2\2\u00e8\u00e9\7\26\2\2\u00e9\63\3\2\2\2\u00ea\u00f0\7\13"+
		"\2\2\u00eb\u00f0\7\f\2\2\u00ec\u00f0\7\34\2\2\u00ed\u00f0\5\66\34\2\u00ee"+
		"\u00f0\7\r\2\2\u00ef\u00ea\3\2\2\2\u00ef\u00eb\3\2\2\2\u00ef\u00ec\3\2"+
		"\2\2\u00ef\u00ed\3\2\2\2\u00ef\u00ee\3\2\2\2\u00f0\65\3\2\2\2\u00f1\u00f2"+
		"\7\23\2\2\u00f2\u00f3\5\64\33\2\u00f3\u00f4\7\24\2\2\u00f4\67\3\2\2\2"+
		"\25>NS_gv\u0081\u0087\u0095\u00a0\u00a7\u00b1\u00b7\u00c2\u00c9\u00d3"+
		"\u00d8\u00e5\u00ef";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}