


-- Crear la base de datos
CREATE DATABASE ExamenFinalProgra3;
GO
USE ExamenFinalProgra3;

-- Crear tabla Agentes
CREATE TABLE Agentes (
    IDAgente INT IDENTITY PRIMARY KEY,
    Nombre VARCHAR(50),
    Email VARCHAR(100),
    Telefono VARCHAR(20),
    Contrasena varchar(50)not null,
	constraint uq_correo UNIQUE(Email)
);

-- Crear tabla Clientes
CREATE TABLE Clientes (
    IDCliente INT IDENTITY PRIMARY KEY,
    Nombre VARCHAR(50),
    Email VARCHAR(100) unique,
    Telefono VARCHAR(20)
);

-- Crear tabla Casas
CREATE TABLE Casas (
    IDCasa INT IDENTITY PRIMARY KEY,
    Direccion VARCHAR(100),
    Ciudad VARCHAR(50),
    Precio DECIMAL(10, 2)
);

-- Crear tabla Ventas
CREATE TABLE Ventas (
    IDVenta INT IDENTITY PRIMARY KEY,
    ID_Agente INT,
    ID_Cliente INT,
    ID_Casa INT,
    Fecha DATE,
    FOREIGN KEY (ID_Agente) REFERENCES Agentes(IDAgente),
    FOREIGN KEY (ID_Cliente) REFERENCES Clientes(IDCliente),
    FOREIGN KEY (ID_Casa) REFERENCES Casas(IDCasa)
);

-- Insertar datos en la tabla Agentes
INSERT INTO Agentes (Nombre, Email, Telefono, Contrasena) VALUES
('Luis Chaves M', 'lacm@lacm.com', '7139-2449', 'lacm123'),
('María López', 'maria@example.com', '987-654-3210', 'prueba2'),
('Carlos González', 'carlos@example.com', '456-789-0123', 'prueba3');

-- Insertar datos en la tabla Clientes
INSERT INTO Clientes (Nombre, Email, Telefono) VALUES
('Laura Martínez', 'laura@example.com', '111-222-3333'),
('Pedro Rodríguez', 'pedro@example.com', '444-555-6666'),
('Ana García', 'ana@example.com', '777-888-9999');

-- Insertar datos en la tabla Casas
INSERT INTO Casas (Direccion, Ciudad, Precio) VALUES
('Calle 123', 'Madrid', 250000.00),
('Avenida 456', 'Barcelona', 300000.00),
('Calle 789', 'Valencia', 200000.00);

-- Insertar datos en la tabla Ventas
INSERT INTO Ventas (ID_Agente, ID_Cliente, ID_Casa, Fecha) VALUES
(1, 2, 1, '2024-04-01'),
(2, 3, 2, '2024-04-03'),
(3, 1, 3, '2024-04-05');

select * from agentes
select * from casas
select * from Clientes
select * from ventas

-- PROCEDURE VALIDAR LOGIN
CREATE PROCEDURE ValidarLogin
    @email VARCHAR(100),
    @contrasena VARCHAR(50)
AS
BEGIN
    SELECT IDAgente AS 'ID Agente', Nombre AS 'Nombre Completo', Email
    FROM Agentes
    WHERE Email = @email 
        AND BINARY_CHECKSUM(Contrasena) = BINARY_CHECKSUM(@contrasena);
END;

-- Crear el procedimiento almacenado GestionarAgentes

CREATE PROCEDURE GestionarAgentes
    @accion NVARCHAR(10),
    @agente_id INT = NULL,
    @agente_nombre NVARCHAR(50) = NULL,
    @agente_email NVARCHAR(100) = NULL,
    @agente_telefono NVARCHAR(20) = NULL,
    @agente_contrasena NVARCHAR(50) = NULL
AS
BEGIN
    IF @accion = 'agregar'
    BEGIN
        INSERT INTO Agentes (Nombre, Email, Telefono, Contrasena) 
        VALUES (@agente_nombre, @agente_email, @agente_telefono, @agente_contrasena);
    END
    ELSE IF @accion = 'borrar'
    BEGIN
        DELETE FROM Agentes WHERE IDAgente = @agente_id;
    END
    ELSE IF @accion = 'modificar'
    BEGIN
        UPDATE Agentes SET 
            Nombre = @agente_nombre,
            Email = @agente_email,
            Telefono = @agente_telefono,
            Contrasena = @agente_contrasena
        WHERE IDAgente = @agente_id;
    END
    ELSE IF @accion = 'consultar'
    BEGIN
        SELECT IDAgente, Nombre, Email, Telefono 
		FROM Agentes
		WHERE IDAgente = @agente_id;
    END
    ELSE
    BEGIN
        SELECT 'Acción no válida';
    END
END;

--- PROCEDURE LLENAR GRID AGENTES 

CREATE PROCEDURE LlenarGridAgentes
AS
BEGIN
    SELECT IDAgente, Nombre, Email, Telefono FROM Agentes;
END;

--- PROCEDURE CONSULTAR UN AGENTE 

CREATE PROCEDURE ConsultarAgentePorID
    @IDAgente INT
AS
BEGIN
    SELECT IDAgente, Nombre, Email, Telefono
    FROM Agentes
    WHERE IDAgente = @IDAgente;
END;

---PROCEDURES PARA GESTIONAR CLIENTES

CREATE PROCEDURE GestionarClientes
    @accion NVARCHAR(10),
    @cliente_id INT = NULL,
    @cliente_nombre NVARCHAR(50) = NULL,
    @cliente_email NVARCHAR(100) = NULL,
    @cliente_telefono NVARCHAR(20) = NULL
AS
BEGIN
    IF @accion = 'agregar'
    BEGIN
        INSERT INTO Clientes (Nombre, Email, Telefono) 
        VALUES (@cliente_nombre, @cliente_email, @cliente_telefono);
    END
    ELSE IF @accion = 'borrar'
    BEGIN
        DELETE FROM Clientes WHERE IDCliente = @cliente_id;
    END
    ELSE IF @accion = 'modificar'
    BEGIN
        UPDATE Clientes SET 
            Nombre = @cliente_nombre,
            Email = @cliente_email,
            Telefono = @cliente_telefono
        WHERE IDCliente = @cliente_id;
    END
    ELSE IF @accion = 'consultar'
    BEGIN
        SELECT IDCliente, Nombre, Email, Telefono 
		FROM Clientes
		WHERE IDCliente = @cliente_id;
    END
    ELSE
    BEGIN
        SELECT 'Acción no válida';
    END
END;

--- LLENAR GRID CLIENTES
CREATE PROCEDURE LlenarGridClientes
AS
BEGIN
    SELECT IDCliente, Nombre, Email, Telefono FROM Clientes;
END;

---PROCEDURES PARA GESTIONAR CASAS

CREATE PROCEDURE GestionarCasas
    @accion NVARCHAR(10),
    @casa_id INT = NULL,
    @casa_direccion VARCHAR(100) = NULL,
    @casa_ciudad VARCHAR(50) = NULL,
    @casa_precio DECIMAL(10, 2) = NULL
AS
BEGIN
    IF @accion = 'agregar'
    BEGIN
        INSERT INTO Casas (Direccion, Ciudad, Precio) 
        VALUES (@casa_direccion, @casa_ciudad, @casa_precio);
    END
    ELSE IF @accion = 'borrar'
    BEGIN
        DELETE FROM Casas WHERE IDCasa = @casa_id;
    END
    ELSE IF @accion = 'modificar'
    BEGIN
        UPDATE Casas SET 
            Direccion = @casa_direccion,
            Ciudad = @casa_ciudad,
            Precio = @casa_precio
        WHERE IDCasa = @casa_id;
    END
    ELSE IF @accion = 'consultar'
    BEGIN
        SELECT IDCasa, Direccion, Ciudad, Precio 
		FROM Casas
		WHERE IDCasa = @casa_id;
    END
    ELSE
    BEGIN
        SELECT 'Acción no válida';
    END
END;


----- LLENAR GRID CASAS

CREATE PROCEDURE LlenarGridCasas
AS
BEGIN
    SELECT IDCasa, Direccion, Ciudad, Precio FROM Casas;
END;


---PROCEDURES PARA GESTIONAR VENTAS

CREATE PROCEDURE GestionarVentas
    @accion NVARCHAR(10),
    @venta_id INT = NULL,
    @agente_id INT = NULL,
    @cliente_id INT = NULL,
    @casa_id INT = NULL,
    @fecha DATE = NULL
AS
BEGIN
    IF @accion = 'agregar'
    BEGIN
        INSERT INTO Ventas (ID_Agente, ID_Cliente, ID_Casa, Fecha) 
        VALUES (@agente_id, @cliente_id, @casa_id, @fecha);
    END
    ELSE IF @accion = 'borrar'
    BEGIN
        DELETE FROM Ventas WHERE IDVenta = @venta_id;
    END
    ELSE IF @accion = 'modificar'
    BEGIN
        UPDATE Ventas SET 
            ID_Agente = @agente_id,
            ID_Cliente = @cliente_id,
            ID_Casa = @casa_id,
            Fecha = @fecha
        WHERE IDVenta = @venta_id;
    END
    ELSE IF @accion = 'consultar'
    BEGIN
        SELECT IDVenta, ID_Agente, ID_Cliente, ID_Casa, Fecha 
		FROM Ventas
		WHERE IDVenta = @venta_id;
    END
    ELSE
    BEGIN
        SELECT 'Acción no válida';
    END
END;

--- PROCEDURE LLENAR GRID VENTAS

CREATE PROCEDURE LlenarGridVentas
AS
BEGIN
    SELECT IDVenta, ID_Agente, ID_Cliente, ID_Casa, Fecha 
    FROM Ventas;
END;
