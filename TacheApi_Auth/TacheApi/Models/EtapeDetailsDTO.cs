using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using TacheApi.OpenApi;

namespace TacheApi.Models
{
    public class EtapeDetailsDTO
    {
        [Required]
        [Description("L'identifiant unique de l'étape")]
        public long Id { get; set; }

        [MaxLength(255)]
        [Description("Le nom de l'étape")]
        [Example("Acheter des bananes")]
        public string? Nom { get; set; }

        [Required]
        [Description("Indique si l'étape est accomplie")]
        [Example("false")]
        public bool EstAccomplie { get; set; }

        [Required]
        [Description("Lien vers la tâche associée à l'étape")]
        public TacheDTO Tache { get; set; }

        public EtapeDetailsDTO(Etape etape)
        {
            if (etape.Tache is null)
            {
                throw new ArgumentNullException("etape.Tache");
            }

            Id = etape.Id;
            Nom = etape.Nom;
            EstAccomplie = etape.EstAccomplie;
            Tache = new TacheDTO(etape.Tache);
        }
    }
}
