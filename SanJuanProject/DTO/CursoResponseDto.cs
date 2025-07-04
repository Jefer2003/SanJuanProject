namespace SanJuanProject.DTO
{
    public class CursoResponseDto
    {
        public int Id { get; set; }
        public string NombreCurso { get; set; }
        public int Creditos { get; set; }
        public int HorasSemanal { get; set; }
        public string Ciclo { get; set; }

        public int IdDocente { get; set; }
        public string NombreDocente { get; set; }
    }
}
