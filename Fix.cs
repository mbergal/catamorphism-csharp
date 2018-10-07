namespace Catamorphism {
    public class Fix<T> {
        public Fix(App<T, Fix<T>> unfix) {
            this.Unfix = unfix;
        }

        public readonly App<T, Fix<T>> Unfix;
    }

    public static class FixStatic {
        public static Fix<B> Fix<B>(App<B, Fix<B>> unfix) {
            return new Fix<B>(unfix);
        }
    }
}