using System;

public interface App<F, A>
{
}

public interface Functor<F>
{
    App<F, B> Map<A, B>(App<F, A> a, Func<A, B> f);
}