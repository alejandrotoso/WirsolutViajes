using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WirsolutViajes.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM VehicleTypes;
                DELETE FROM VehicleSubtypes;
                DELETE FROM Brands;
                DELETE FROM BrandVehicleTypes;
                DELETE FROM Models;

                SET IDENTITY_INSERT  [VehicleTypes] OFF;
                SET IDENTITY_INSERT  [VehicleSubtypes] OFF;
                SET IDENTITY_INSERT  [Brands] OFF;
                SET IDENTITY_INSERT  [BrandVehicleTypes] OFF;
                SET IDENTITY_INSERT  [Models] OFF;

                BEGIN TRY
                    BEGIN TRANSACTION;


					SET IDENTITY_INSERT  [VehicleTypes] ON;
					INSERT INTO VehicleTypes (Id, [Name]) VALUES
					(1, 'Camión'),
					(2, 'Furgoneta'),
					(3, 'Auto'),
					(4, 'Moto'),
					(5, 'Vehículo de carga liviana');
					SET IDENTITY_INSERT [VehicleTypes] OFF;


					SET IDENTITY_INSERT  [VehicleSubtypes] ON;
					INSERT INTO VehicleSubtypes (Id, [Name], VehicleTypeId) VALUES
					-- Camión
					(1, 'Camión cisterna', 1),
					(2, 'Camión de carga seca', 1),
					(3, 'Camión volquete', 1),
					(4, 'Camión con plataforma', 1),
					(5, 'Camión grúa', 1),
					(6, 'Camión refrigerado', 1),
					-- Furgoneta
					(7, 'Furgoneta de reparto', 2),
					(8, 'Furgoneta de carga pesada', 2),
					(9, 'Furgoneta refrigerada', 2),
					(10, 'Furgoneta de mensajería', 2),
					-- Auto
					(11, 'Sedán', 3),
					(12, 'Hatchback', 3),
					(13, 'Auto compacto', 3),
					(14, 'Auto deportivo', 3),
					-- Moto
					(15, 'Motocicleta de reparto', 4),
					(16, 'Motocicleta de carga', 4),
					(17, 'Motocicleta mensajera', 4),
					-- Vehículo de carga liviana
					(18, 'Van de carga pequeña', 5),
					(19, 'Van de carga mediana', 5),
					(20, 'Van de carga grande', 5),
					(21, 'Minivan de transporte de pasajeros', 5),
					(22, 'Minivan de carga ligera', 5);
					SET IDENTITY_INSERT  [VehicleSubtypes] OFF;


					SET IDENTITY_INSERT  [Brands] ON;
					INSERT INTO Brands (Id, [Name]) VALUES
					(1, 'Volvo'), -- Camión
					(2, 'Scania'), -- Camión
					(3, 'Ford'), -- Furgoneta, Auto, Vehículo de carga liviana
					(4, 'Volkswagen'), -- Auto, Furgoneta, Vehículo de carga liviana
					(5, 'Renault'), -- Furgoneta, Vehículo de carga liviana
					(6, 'Toyota'), -- Auto, Vehículo de carga liviana
					(7, 'Honda'), -- Auto, Moto
					(8, 'Yamaha'); -- Moto
					SET IDENTITY_INSERT  [Brands] OFF;


					SET IDENTITY_INSERT [BrandVehicleTypes] ON;
					-- Camión
					INSERT INTO BrandVehicleTypes (Id, BrandId, VehicleTypeId) VALUES
					(1, 1, 1), -- Volvo
					(2, 2, 1), -- Scania
					-- Furgoneta
					(3, 3, 2), -- Ford
					(4, 4, 2), -- Volkswagen
					(5, 5, 2), -- Renault
					-- Auto
					(6, 3, 3), -- Ford
					(7, 4, 3), -- Volkswagen
					(8, 6, 3), -- Toyota
					-- Moto
					(9, 7, 4), -- Honda
					(10, 8, 4), -- Yamaha
					-- Vehículo de carga liviana
					(11, 4, 5), -- Volkswagen
					(12, 5, 5); -- Renault
					SET IDENTITY_INSERT [BrandVehicleTypes] OFF;


					SET IDENTITY_INSERT [Models] ON;
					-- Insertar Camión 
					INSERT INTO [Models] ([Id], [Name], [BrandVehicleTypeId]) VALUES
					-- Volvo Camión (Id 1)
					(1, 'NL10', 1),		
					(2, 'FH16', 1),		
					(3, 'FMX', 1),		
					-- Insertar Scania Camión (Id 2)
					(4, 'Serie R', 2),	
					(5, 'Serie S', 2),
					(6, 'Serie P', 2),
					-- Insertar Furgoneta 
					-- Insertar Ford Furgoneta (Id 3)
					(7, 'Transit', 3),
					(8, 'Tourneo Custom', 3),
					(9, 'Connect', 3),
					-- Insertar Volkswagen Furgoneta (Id 4)
					(10, 'Crafter', 4),   
					-- Insertar Renault Furgoneta (Id 5)
					(11, 'Trafic', 5),
					-- Insertar Auto 
					-- Insertar Ford (Id 6)
					(12, 'Ka', 6),
					-- Insertar Volkswagen (Id 7)
					(13, 'Gol', 7),   
					-- Insertar Toyota (Id 8)
					(14, 'Corolla', 8),
					-- Insertar Moto 
					-- Insertar Honda (Id 9)
					(15, 'CB125F', 9),
					-- Insertar Yamaha (Id 10)
					(16, 'YS125', 10),   
					-- Insertar Vehículo de carga liviana 
					-- Insertar Volkswagen (Id 11)
					(17, 'Caddy', 11),
					-- Insertar Renault (Id 12)
					(18, 'Kangoo', 12);   
					SET IDENTITY_INSERT [Models] OFF;



                    COMMIT TRANSACTION;
                END TRY
                BEGIN CATCH
                    ROLLBACK TRANSACTION;
                    PRINT 'Error: ' + ERROR_MESSAGE();
                END CATCH;
            ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
