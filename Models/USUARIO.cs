using System.ComponentModel.DataAnnotations;

namespace SistemaJuridicoWebAPI.Models
{
    public class USUARIO
    {
        [Key]
        public Guid ID_USUARIO { get; set; }

        public string NOME_USUARIO { get; set; }

        public string SENHA { get; set; }

        public int ACESSO_GESTAO { get; set; }
    }
}
