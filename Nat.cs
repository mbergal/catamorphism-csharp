using System;
using Catamorphism;
using static NatStatic;

public class NatBrand {
}

public abstract class Nat<A> {
    public abstract R Match<R>(Func<A, R> succ, Func<R> zero);
}

public class Succ<A> : Nat<A>, App<NatBrand, A> {
    private readonly A previous;

    public Succ(A previous) {
        this.previous = previous;
    }

    public override R Match<R>(Func<A, R> succ, Func<R> zero) {
        return succ(this.previous);
    }
}

public class Zero<A> : Nat<A>, App<NatBrand, A> {
    public override R Match<R>(Func<A, R> succ, Func<R> zero) {
        return zero();
    }
};


public class NatFunctor : Functor<NatBrand> {
    public App<NatBrand, B> Map<A, B>(App<NatBrand, A> fa, Func<A, B> f) {
        var a = (Nat<A>) fa;
        return a.Match<App<NatBrand, B>>(
            succ: x => Succ<B>(f(x)),
            zero: () => Zero<B>());
    }
}


public static class NatStatic {
    public static NatFunctor natFunctor = new NatFunctor();
    
    public static Succ<A> Succ<A>(A previous) {
        return new Succ<A>(previous);
    }

    public static Zero<A> Zero<A>() {
        return new Zero<A>();
    }

}