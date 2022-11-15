﻿using System;
using System.Collections.Generic;

namespace SGPI_Andres.Models
{
    public partial class Entrevistum
    {
        public int IdEntrevista { get; set; }
        public int IdUsuario { get; set; }
        public int IdEstudiante { get; set; }
        public DateTime Fecha { get; set; }

        public virtual Estudiante IdEntrevistaNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}
