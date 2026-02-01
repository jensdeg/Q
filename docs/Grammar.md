<h2 style="text-align:center;"><b>Program</b></h2>

$$

\begin{align*}
    \text{[Program]} &\to [\text{Stmt}]^* \text{ EOF} \\
\end{align*}

$$

<h2 style="text-align:center;"><b>Statements</b></h2>

$$

\begin{align*}
    \text{[Stmt]} &\to
    \begin{cases}
        \text{[Print]} \\
        \text{[Var]} \\
        \text{[ExprStmt]} \\
    \end{cases} \\

    \text{[ExprStmt]} &\to \text{[Expr]} \text{ ';' } \\

    \text{[Print]} &\to Print \text{ '(' } \text{[Expr]}\text{ ')' }  \text{ ';' } \\

    \text{[Var]} &\to var \text{ IDENTIFIER '=' [Expr]} \text{ ';' } \\
\end{align*}

$$

<h2 style="text-align:center;"><b>Expressions</b></h2>

$$

\begin{align*}
        \text{[Expr]} &\to \text{[Term]} \\

        \text{[Term]} &\to \text{[Factor] } \text{(('+' | '-') [Factor])}^* \\

        \text{[Factor]} &\to \text{[Primary]} \text{(('*' | '/') [Primary])}^* \\

        
\end{align*}

$$

<h2 style="text-align:center;"><b>Basic Values</b></h2>

$$

\begin{align*}
    \text{[Primary]} &\to 
        \begin{cases}
            \text{NUMBER} \\
            \text{STRING} \\
            \text{IDENTIFIER} \\
            \text{ '(' } \text{[Expr]} \text{ ')' }  \\
        \end{cases} \\
\end{align*}

$$

