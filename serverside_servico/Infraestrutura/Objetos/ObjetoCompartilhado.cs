using System;

namespace serverside_servico.Infraestrutura.Objetos
{
    [Serializable]
    public abstract class ObjetoCompartilhado
    {
        private int? _hashCode;

        public virtual Guid Id { get; set; }

        public override bool Equals(object obj)
        {
            var objetoPersistido = obj as ObjetoCompartilhado;

            if (objetoPersistido == null)
            {
                return false;
            }

            if (objetoPersistido.Id == Guid.Empty || Id == Guid.Empty)
            {
                return this == objetoPersistido;
            }

            return Id.Equals(objetoPersistido.Id);
        }

        public override int GetHashCode()
        {
            if (!_hashCode.HasValue)
            {
                var _hashCodeBase = Id == Guid.Empty ? base.GetHashCode() : Id.GetHashCode();

                _hashCode = string.Concat("ObjetoCompartilhado", _hashCodeBase).GetHashCode();
            }

            return _hashCode.Value;
        }

        public static bool operator ==(ObjetoCompartilhado x, ObjetoCompartilhado y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(ObjetoCompartilhado x, ObjetoCompartilhado y)
        {
            return !(x == y);
        }
    }
}
