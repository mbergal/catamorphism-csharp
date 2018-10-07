using System;

namespace Catamorphism {
    using static Morphisms;
    using static FixStatic;
    using static NatStatic;
    using static IntListStatic;

    public static class Morphisms {
        public static A Cata<F, A>(Fix<F> structure, Func<App<F, A>, A> algebra, Functor<F> aa) {
            var a = structure.Unfix;

            return algebra(aa.Map(a, x => Cata(x, algebra, aa)));
        }
    }

    public static class Program {
        public static void Main() {
            var a = Fix(Succ(Fix(Succ(Fix(Zero<Fix<NatBrand>>())))));
            var b = Cata<NatBrand, int>(a, NatToInt, natFunctor);
            Console.WriteLine(b);

            var lst = Fix(Cons(1,
                Fix(Cons(2,
                    Fix(Cons(3,
                        Fix(Empty<Fix<IntListBrand>>()))
                    ))
                ))
            );

            var c = Cata<IntListBrand, int>(lst,
                unfix => 
                     IntListBrand.Project(unfix).Match(
                        cons: (head, tail) => head + tail, 
                        empty: () => 0),
                intListFunctor);
            
            Console.WriteLine(c);
        }

        public static int NatToInt(App<NatBrand, int> unfix) {
            var a = (Nat<int>) unfix;
            return a.Match(
                succ: x => x + 1,
                zero: () => 0
            );
        }
    }
}