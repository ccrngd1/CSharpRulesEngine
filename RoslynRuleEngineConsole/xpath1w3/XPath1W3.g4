grammar XPath1W3;

/*
XPath 1.0 from w3c spec
http://www.w3.org/2013/01/qt-applets/xpath1/jjdoc.html
*/



start  :  exprSingle | EOF
;


exprSingle  :  orExpr
;

operatorExpr : orExpr
;

orExpr  :  andExpr ('or' andExpr)*
;

andExpr  :  equalityExpr ('and' equalityExpr)*
;

equalityExpr
:  relationalExpr (('='|'!=') relationalExpr)*
;

relationalExpr
:  additiveExpr (('<'|'>'|'<='|'>=') additiveExpr)*
;

additiveExpr
:  multiplicativeExpr (('+'|'-') multiplicativeExpr)*
;

multiplicativeExpr
:  unaryExpr (('*'|'div'|'mod') unaryExpr)?
/*|  '/' (('div'|'mod') multiplicativeExpr)?*/
;

unaryExpr
:  '-'* unionExpr
;

unionExpr
:  valueExpr ('|' valueExpr)*
;

valueExpr
: (((primaryExpr (predicate)*) (('/'|'//')relativePathExpr)?)|pathExpr)
;

pathExpr
: (('/' (relativePathExpr)?) | ('//' relativePathExpr) | relativePathExpr)
;

relativePathExpr
: stepExpr (( '//' | '/' ) stepExpr)*
;

stepExpr
: (axisStep | contextItemExpr | abbrevReverseStep)
;

axisStep
:(reverseStep | forwardStep) (predicate)*
;

forwardStep
:(( forwardAxis nodeTest) | abbrevForwardStep)
;

forwardAxis
:(('child' '::') | ('descendant' '::' ) | ('attribute' '::') | ('self' '::')  | ( 'descendant-or-self' '::' ) | ( 'following-sibling' '::' ) | ( 'following' '::' ) | ( 'namespace' '::' ) )
;

abbrevForwardStep
:('@')? nodeTest
;

reverseStep
:(reverseAxis nodeTest)
;

reverseAxis
:('parent' '::') | ('ancestor' '::') | ('preceding-sibling' '::') | ('preceding' '::') | ('ancestor-or-self' '::')
;

abbrevReverseStep
:'..'
;

nodeTest: kindTest
  | nameTest
  ;

nameTest
:qName | wildCard
;

wildCard
:('*' | NCName ':' '*')
;

predicate
:'[' exprSingle ']'
;

primaryExpr
:(Literal | varRef | parenthesizedExpr | functionCall )
;

Literal
  : (NumericLiteral | StringLiteral)
  ;

NumericLiteral
:IntegerLiteral | DecimalLiteral
;

varRef
:'$' varName
;

varName : qName;

parenthesizedExpr
:'(' exprSingle ')'
;

contextItemExpr
:'.'
;

functionCall
:functionQName ( '(' (exprSingle (',' exprSingle)* )? ')' )
;

kindTest
:(PITest | commentTest | textTest | anyKindTest)
;

anyKindTest
:'node' '(' ')'
;

textTest
:'test' '(' ')'
;

commentTest
:'comment' '(' ')'
;

PITest
:'processing-instruction' '(' (StringLiteral)? ')'
;

StringLiteral
: ('\''|'\"') (((Digits)*) ((NCNameChar)))+ ('\''|'\"')
;

IntegerLiteral
: (Digits)+
;

DecimalLiteral
: (Digits)+ '.' (Digits)+
;

nCName  :  NCName
  |  AxisName
;

qName  :  nCName (':' nCName)?
  ;

functionQName
:(qName | 'ancestor' | 'ancestor-or-self' | 'and' | 'attribute' | 'child' | 'descendant' | 'descendant-or-self' | 'div' | 'following' | 'following-sibling' | 'mod' | 'namespace' | 'or' | 'parent' | 'preceding' | 'preceding-sibling' | 'self')
;

Number  :  Digits ('.' Digits?)?
  |  '.' Digits
  ;


Digits  :  ('0'..'9')+
  ;

AxisName:  'ancestor'
  |  'ancestor-or-self'
  |  'attribute'
  |  'child'
  |  'descendant'
  |  'descendant-or-self'
  |  'following'
  |  'following-sibling'
  |  'namespace'
  |  'parent'
  |  'preceding'
  |  'preceding-sibling'
  |  'self'
  ;

LiteralChar  :  '"' ~'"'* '"'
  |  '\'' ~'\''* '\''
  ;

WS
  :  (' '|'\t'|'\n'|'\r')+ -> skip
  ;

NCName  :  NCNameStartChar NCNameChar*
  ;

NCNameStartChar
  :  'A'..'Z'
  |   '_'
  |  'a'..'z'
  |  '\u00C0'..'\u00D6'
  |  '\u00D8'..'\u00F6'
  |  '\u00F8'..'\u02FF'
  |  '\u0370'..'\u037D'
  |  '\u037F'..'\u1FFF'
  |  '\u200C'..'\u200D'
  |  '\u2070'..'\u218F'
  |  '\u2C00'..'\u2FEF'
  |  '\u3001'..'\uD7FF'
  |  '\uF900'..'\uFDCF'
  |  '\uFDF0'..'\uFFFD'
// Unfortunately, java escapes can't handle this conveniently,
// as they're limited to 4 hex digits. TODO.
//  |  '\U010000'..'\U0EFFFF'
  ;


NCNameChar
  :  NCNameStartChar | '-' | '.' | '0'..'9'
  |  '\u00B7' | '\u0300'..'\u036F'
  |  '\u203F'..'\u2040'
  ;