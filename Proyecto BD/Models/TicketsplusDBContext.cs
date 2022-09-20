using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Proyecto_BD.Models
{
    public partial class TicketsplusDBContext : DbContext
    {
        public TicketsplusDBContext()
        {
        }

        public TicketsplusDBContext(DbContextOptions<TicketsplusDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Evento> Eventos { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Ticketxcliente> Ticketxclientes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=TAVO-PC;user=sa;password=1234;Database=TicketsplusDB;Trusted_Connection=True;encrypt=false;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("PK__CLIENTE__23A34130EC4746A5");

                entity.ToTable("CLIENTE");

                entity.Property(e => e.IdCliente).HasColumnName("ID_CLIENTE");

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CORREO");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<Evento>(entity =>
            {
                entity.HasKey(e => e.IdEvento)
                    .HasName("PK__EVENTO__748D7F35E00EF3B7");

                entity.ToTable("EVENTO");

                entity.Property(e => e.IdEvento).HasColumnName("ID_EVENTO");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.Fecha)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FECHA");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => e.IdTicket)
                    .HasName("PK__TICKET__129B4F227134D4CC");

                entity.ToTable("TICKET");

                entity.Property(e => e.IdTicket).HasColumnName("ID_TICKET");

                entity.Property(e => e.IdCliente).HasColumnName("ID_CLIENTE");

                entity.Property(e => e.IdEvento).HasColumnName("ID_EVENTO");

                entity.Property(e => e.Qr)
                    .HasColumnType("image")
                    .HasColumnName("QR");

                entity.HasOne(d => d.IdEventoNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdEvento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TICKET__ID_EVENT__29572725");
            });

            modelBuilder.Entity<Ticketxcliente>(entity =>
            {
                entity.HasKey(e => e.IdTicketxCliente);

                entity.ToTable("TICKETXCLIENTE");

                entity.Property(e => e.IdTicketxCliente).HasColumnName("ID_TicketxCliente");

                entity.Property(e => e.IdCliente).HasColumnName("ID_CLIENTE");

                entity.Property(e => e.IdTicket).HasColumnName("ID_TICKET");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Ticketxclientes)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TICKETXCL__ID_CL__2A4B4B5E");

                entity.HasOne(d => d.IdTicketNavigation)
                    .WithMany(p => p.Ticketxclientes)
                    .HasForeignKey(d => d.IdTicket)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TICKETXCL__ID_TI__2A4B4B5E");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
