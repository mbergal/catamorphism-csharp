using System;

namespace Catamorphism {
    using static Morphisms;
    using static FixStatic;
    using static NatStatic;
    using static IntListStatic;
    using static ExprStatic;

    public static class Morphisms {
        public static A Cata<F, A>(Fix<F> structure, Func<App<F, A>, A> algebra, Functor<F> aa) {
            var a = structure.Unfix;

            return algebra(aa.Map(a, x => Cata(x, algebra, aa)));
        }
    }

    public static class Program {
        public static void Main() {
            TestNatFix();
            TestIntListFix();
            TestExprFix();
        }

        private static void TestExprFix() {
            var d =
                Fix(Add(
                    Fix(Mult(
                        Fix(Num<Fix<ExprBrand>>(2)),
                        Fix(Num<Fix<ExprBrand>>(3))
                    )),
                    Fix(Num<Fix<ExprBrand>>(3))
                ));
        }

        private static void TestIntListFix() {
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

        private static void TestNatFix() {
            var a = Fix(Succ(Fix(Succ(Fix(Zero<Fix<NatBrand>>())))));
            var b = Cata<NatBrand, int>(a, NatToInt, natFunctor);
            Console.WriteLine(b);
        }


        private static int NatToInt(App<NatBrand, int> unfix) {
            var a = (Nat<int>) unfix;
            return a.Match(
                succ: x => x + 1,
                zero: () => 0
            );
        }
    }
}