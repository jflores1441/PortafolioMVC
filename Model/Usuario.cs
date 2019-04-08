namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Data.Entity.Validation;
    using System.IO;
    using System.Linq;//Para poder hacer las consultas a la base de datos
    using System.Web;//Para poder usar HttpPostedFileBase Foto
    using Helper;//Para usar el HashHelper

    [Table("Usuario")]
    public partial class Usuario
    {
        public Usuario()
        {
            Experiencia = new HashSet<Experiencia>();
            Habilidad = new HashSet<Habilidad>();
            Testimonio = new HashSet<Testimonio>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string Apellido { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(32)]
        public string Password { get; set; }

        [Column(TypeName = "text")]
        public string Direccion { get; set; }

        [StringLength(50)]
        public string Ciudad { get; set; }

        public int? Pais_id { get; set; }

        [StringLength(50)]
        public string Telefono { get; set; }

        [StringLength(100)]
        public string Facebook { get; set; }

        [StringLength(100)]
        public string Twitter { get; set; }

        [StringLength(100)]
        public string YouTube { get; set; }

        [StringLength(50)]
        public string Foto { get; set; }

        public virtual ICollection<Experiencia> Experiencia { get; set; }

        public virtual ICollection<Habilidad> Habilidad { get; set; }

        public virtual ICollection<Testimonio> Testimonio { get; set; }

        public ResponseModel Acceder(string Email, string Password)
        {
            var rm = new ResponseModel();

            try
            {
                using( var ctx = new proyectoContext())
                {
                    Password = HashHelper.MD5(Password);
                    //Validar que el usuario y la contraseña se hayan escrito correctamente
                    var usuario = ctx.Usuario.Where(x => x.Email == Email)
                                             .Where(x => x.Password == Password)
                                             .SingleOrDefault();

                    if (usuario != null)
                    {
                        SessionHelper.AddUserToSession(usuario.id.ToString());
                        rm.SetResponse(true);
                    }
                    else
                    {
                        rm.SetResponse(false, "Usuario o contraseña incorrecta");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return rm;
        } 

        public Usuario ObtenerUsuario(int id)
        {
            var usuario = new Usuario();

            try
            {
                using(var ctx= new proyectoContext())
                {
                    usuario = ctx.Usuario.Where(x => x.id == id).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return usuario;
        }

        public ResponseModel Guardar(HttpPostedFileBase Foto)
        {
            var rm = new ResponseModel();

            try
            {
                using(var ctx= new proyectoContext())
                {              
                    ctx.Configuration.ValidateOnSaveEnabled = false;//Indicarle al contexto que las validaciones cuando se guarde, esten desabilitadas, debido a que queremos que ignore la propiedad Password

                    var eUsuario = ctx.Entry(this);
                    eUsuario.State = EntityState.Modified;

                    if(this.Password == null)
                        eUsuario.Property(x => x.Password).IsModified = false;//Le indicamos que queremos que ignore la propiedad Password, y que no la valide

                    if (Foto != null)
                    {
                        //System.IO para usar Path
                        string archivo = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(Foto.FileName);

                        Foto.SaveAs(HttpContext.Current.Server.MapPath("~/Uploads/" + archivo));

                        this.Foto = archivo;
                    }
                    else
                        eUsuario.Property(x => x.Foto).IsModified = false;

                    ctx.SaveChanges();
                    rm.SetResponse(true);
                }
            }

            //Referencia using System.Data.Entity.Validation;
            //Podemos ver más a detalle la excepcion del EF 
            //catch (DbEntityValidationException e)
            //{
            //    throw;
            //}

            catch (Exception)
            {

                throw;
            }

            return rm;
        }
    }
}
