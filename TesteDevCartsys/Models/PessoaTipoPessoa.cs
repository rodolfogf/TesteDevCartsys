namespace TesteDevCartsys.Models;

public class PessoaTipoPessoa
{
    public int? PessoaId { get; set; }
    public virtual Pessoa Pessoa { get; set; }
    public int? TipoPessoaId { get; set; }
    public virtual TipoPessoa TipoPessoa { get; set; }
}
