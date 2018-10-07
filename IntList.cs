using System;

namespace Catamorphism
{
    using static IntListStatic;
    public interface IntList<A> : App<IntListBrand, A>{
        R Match<R>(Func<int, A, R> cons, Func<R> empty);
    }


    public class Cons<A> : IntList<A> {
        private int head;
        private A tail;

        public Cons(int head, A tail) {
            this.head = head;
            this.tail = tail;
        }

        public R Match<R>(Func<int, A, R> cons, Func<R> empty) {
            return cons(head, tail);
        }
    }

    public class Empty<A>: IntList<A> {
        public R Match<R>(Func<int, A, R> cons, Func<R> empty) {
            return empty();
        }
    }

    public class IntListBrand  {
        public static App<IntListBrand, A> Inject<A>(IntList<A> a ) {
            return a;
        }

        public static IntList<A> Project<A>(App<IntListBrand, A> a) {
            return (IntList<A>) a;
        }
    };
    
    public class IntListFunctor: Functor<IntListBrand> {
        public App<IntListBrand, B> Map<A, B>(App<IntListBrand, A> fa, Func<A, B> f) {
            return IntListBrand.Project(fa).Match<IntList<B>>(
                cons: (head, tail) => Cons(head, f(tail)),
                empty: ()=>Empty<B>()
            );
        }
    }
    
    public static class IntListStatic {
        public static IntListFunctor intListFunctor = new IntListFunctor();

        public static Cons<A> Cons<A>(int head, A tail) {
            return new Cons<A>(head, tail);
        }
        public static Empty<A> Empty<A>() {
            return new Empty<A>();
        }
    } 
}