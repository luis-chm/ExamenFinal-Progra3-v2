﻿<%@ Page Title="Casas" Language="C#" MasterPageFile="~/Capa Presentacion/Menu Master/Menu.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Casas_View.aspx.cs" Inherits="ExamenFinal.Capa_Presentacion.Casas.Casas_View" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href='https://fonts.googleapis.com/css?family=Roboto:400,100,300,700' rel='stylesheet' type='text/css'>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
    <link rel="stylesheet" href="css/style.css">
    <link href="css/Gridview.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

            <br />
    <div class="container" style="max-width: 750px; min-width: 500px; float: left; margin-left: 100px; margin-right: 50px; margin-bottom: 500px;">
        <div class="card">
            <h3 class="card-header text-center">Registrar Casas</h3>
            <div class="card-body">
                <form id="form1" class="float-end">
                    <div class="row">
                        <!-- Primera columna -->
                        <div class="col">
                            <div class="form-group">
                                <div class="mb-3">
                                    <label class="form-label">ID Casa: </label>
                                    <asp:TextBox runat="server" class="form-control form-control-sm" ID="tCasaID" style="width: 200px; height: 30px !important;"></asp:TextBox>
                                </div>
                                <div class="mb-3">
                                    <label class="form-label">Direccion: </label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="tDireccion"></asp:TextBox>
                                </div>
                                <div class="mb-3">
                                    <label class="form-label">Ciudad: </label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="tCiudad"></asp:TextBox>
                                </div>
                                <div class="mb-3">
                                    <label class="form-label">Precio: </label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="tPrecio" TextMode="Number"></asp:TextBox>
                                </div>
                                <hr />
                                <asp:Button ID="btnAgregar" class="btn btn-success" runat="server" Text="Agregar" Onclick="btnAgregar_Click" />
                                <asp:Button ID="btnModificar" runat="server" class="btn btn-dark" Text="Modificar" OnClick="btnModificar_Click" />
                                <asp:Button ID="btnEliminar" class="btn btn-danger" runat="server" Text="Borrar" OnClick="btnEliminar_Click" />
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>

    <!-- Fuera del formulario -->
    <div class="col-md-6" style="float: right;">
        <div class="card">
            <h3 class="card-header text-center">Casas Registradas</h3>
            <div class="search-box">
                <asp:TextBox runat="server" class="form-control form-control-sm" ID="txConsultarID" style="width: 200px; height: 30px !important; border: 0.01px solid;"></asp:TextBox>
                <asp:Button ID="btnConsultar" runat="server" class="btn btn-info btn-sm" Text="Consultar" Style="color: white;" OnClick="btnConsultar_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnReload" class="btn btn-primary btn-sm" runat="server" Text="Refrescar" OnClick="btnReload_Click"/>

            </div>
            <div class="card-body">
                <hr />
                <!-- Línea divisoria -->
                <!-- GridView -->
                <br />
                <asp:GridView ID="gridCasas" runat="server" AutoGenerateColumns="False" Width="850px">
                    <columns>
                        <asp:BoundField DataField="IDCasa" HeaderText="ID Casa" />
                        <asp:BoundField DataField="Direccion" HeaderText="Direcciono" />
                        <asp:BoundField DataField="Ciudad" HeaderText="Ciudad" />
                        <asp:BoundField DataField="Precio" HeaderText="Precio" />
                    </columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    </div>
</div>

</asp:Content>
