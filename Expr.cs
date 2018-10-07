using System;
using Catamorphism;

class ExprBrand {
}

interface Expr<A> : App<ExprBrand,A>{
    R Match<R>(Func<A, A> add, Func<A, A> mult, Func<int> num);
}


public class Add<A> : Expr<A> {
    public Add(A expr1, A expr2) {
    }

    public R Match<R>(Func<A, A> add, Func<A, A> mult, Func<int> num) {
        throw new NotImplementedException();
    }
}

public class Mult<A> : Expr<A> {
    public Mult(A expr1, A expr) {
    }

    public R Match<R>(Func<A, A> add, Func<A, A> mult, Func<int> num) {
        throw new NotImplementedException();
    }
}

public class Num<A> : Expr<A> {
    public Num(int literal) {
    }

    public R Match<R>(Func<A, A> add, Func<A, A> mult, Func<int> num) {
        throw new NotImplementedException();
    }
}

public static class ExprStatic {
    public static Num<A> Num<A>(int literal) {
        return new Num<A>(literal);
    }

    public static Add<A> Add<A>(A e1, A e2) {
        return new Add<A>(e1, e2);
    }

    public static Mult<A> Mult<A>(A e1, A e2) {
        return new Mult<A>(e1, e2);
    }
}