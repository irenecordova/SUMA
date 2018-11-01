﻿Imports System.ComponentModel

Public Class frmventas

    Private dt As New DataTable

    Private Sub btbuscarc_Click(sender As Object, e As EventArgs) Handles btbuscarc.Click
        txtnombrec.Enabled = False
        txtapellidoc.Enabled = False
        checkgenerico.Checked = False
        checkgenerico.Enabled = True
        frmcliente.txtbandera.Text = "1"
        frmcontenedor.pnpantallas.Controls.Clear()
        frmcliente.TopLevel = False
        frmcliente.Visible = True
        frmcontenedor.pnpantallas.Controls.Add(frmcliente)
        frmcliente.Show()
    End Sub

    Private Sub btbuscare_Click(sender As Object, e As EventArgs) Handles btbuscare.Click
        frmempleado.txtbandera.Text = "1"
        frmcontenedor.pnpantallas.Controls.Clear()
        frmempleado.TopLevel = False
        frmempleado.Visible = True
        frmcontenedor.pnpantallas.Controls.Add(frmempleado)
        frmempleado.Show()
    End Sub

    Private Sub btbuscars_Click(sender As Object, e As EventArgs) Handles btbuscars.Click
        frmservicio.txtbandera.Text = "1"
        frmcontenedor.pnpantallas.Controls.Clear()
        frmservicio.TopLevel = False
        frmservicio.Visible = True
        frmcontenedor.pnpantallas.Controls.Add(frmservicio)
        frmservicio.Show()
    End Sub

    Private Sub checkgenerico_CheckedChanged(sender As Object, e As EventArgs) Handles checkgenerico.CheckedChanged
        If checkgenerico.Checked = True Then
            txtnombrec.Text = "Genérico"
            txtapellidoc.Text = "Genérico"
            txtidcliente.Text = 1
            txtnombrec.Enabled = False
            txtapellidoc.Enabled = False
            checkgenerico.Enabled = False
        End If
    End Sub

    Private Sub btbuscarp_Click(sender As Object, e As EventArgs) Handles btbuscarp.Click
        frmproducto.txtbandera.Text = "1"
        frmcontenedor.pnpantallas.Controls.Clear()
        frmproducto.TopLevel = False
        frmproducto.Visible = True
        frmcontenedor.pnpantallas.Controls.Add(frmproducto)
        frmproducto.Show()
    End Sub

    Private Sub modificar_columnas()

        dgvlistado.CurrentRow.Selected = False


        'Ocultas
        dgvlistado.Columns("idventa").Visible = False
        dgvlistado.Columns("idcliente").Visible = False
        dgvlistado.Columns("fecha").Visible = False
        dgvlistado.Columns("hora").Visible = False
        dgvlistado.Columns("reserva").Visible = False

        'Visibles
        dgvlistado.Columns("nombre").HeaderText = "Nombres"
        dgvlistado.Columns("nombre").Width = 150
        dgvlistado.Columns("apellido").HeaderText = "Apellidos"
        dgvlistado.Columns("apellido").Width = 150
        dgvlistado.Columns("total").HeaderText = "Total"
        dgvlistado.Columns("total").Width = 80

    End Sub

    Private Sub modificar_columnasservicios()

        dgvservicios.CurrentRow.Selected = False

        'Ocultas
        dgvservicios.Columns("iddetalleservicio").Visible = False
        dgvservicios.Columns("idventa").Visible = False
        dgvservicios.Columns("idempleado").Visible = False
        dgvservicios.Columns("idservicio").Visible = False
        dgvservicios.Columns("nombre").Visible = False
        dgvservicios.Columns("apellido").Visible = False

        'Visibles
        dgvservicios.Columns("descripcion").HeaderText = "Descripción"
        dgvservicios.Columns("descripcion").Width = 150
        dgvservicios.Columns("cantidad").HeaderText = "Cantidad"
        dgvservicios.Columns("cantidad").Width = 80
        dgvservicios.Columns("preciounitario").HeaderText = "Precio Unitario"
        dgvservicios.Columns("preciounitario").Width = 150
    End Sub

    Private Sub modificar_columnasproductos()


        dgvproductos.CurrentRow.Selected = False

        'Ocultas
        dgvproductos.Columns("iddetalleproducto").Visible = False
        dgvproductos.Columns("idventa").Visible = False
        dgvproductos.Columns("idproducto").Visible = False

        'Visibles

        dgvproductos.Columns("nombre").HeaderText = "Nombre"
        dgvproductos.Columns("nombre").Width = 150
        dgvproductos.Columns("cantidad").HeaderText = "Cantidad"
        dgvproductos.Columns("cantidad").Width = 80
        dgvproductos.Columns("preciounitario").HeaderText = "Precio Unitario"
        dgvproductos.Columns("preciounitario").Width = 150

    End Sub



    Private Sub btnguardar_Click(sender As Object, e As EventArgs) Handles btnguardar.Click
        If Me.ValidateChildren = True And txtidcliente.Text <> "" And txtnombrec.Text <> "" Then
            Try
                Dim dts As New vventa
                Dim func As New fventa

                dts.gidcliente = txtidcliente.Text
                dts.gfecha = Format(dtpfecha.Value, "dd/MM/yyyy")
                'dts.ghora = Nothing
                dts.gtotal = 0
                dts.greserva = 0

                If func.ingresar(dts) Then
                    MessageBox.Show("Registro completado", "Guardando Registro", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    mostrar()
                    limpiar()
                Else
                    MessageBox.Show("No se pudo completar el registro", "Guardando Registro", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    mostrar()
                    limpiar()
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MessageBox.Show("Datos incompletos. Llene los campos obligatorios", "Guardando Registro", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub frmventas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        mostrar()
    End Sub

    Private Sub mostrar()
        Try
            Dim func As New fventa
            dt = func.mostrar()

            If dt.Rows.Count <> 0 Then
                dgvlistado.DataSource = dt
                txtbuscar.Enabled = True
                dgvlistado.ColumnHeadersVisible = True
                modificar_columnas()
            Else
                dgvlistado.DataSource = Nothing
                txtbuscar.Enabled = False
                dgvlistado.ColumnHeadersVisible = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub mostrarservicios()
        Try
            Dim func As New fdetalleservicio
            Dim dts As New vdetalleservicio


            dts.gidventa = txtidventa.Text

            dt = func.mostrar(dts)

            If dt.Rows.Count <> 0 Then
                dgvservicios.DataSource = dt
                dgvservicios.ColumnHeadersVisible = True
                modificar_columnasservicios()
            Else
                dgvservicios.DataSource = Nothing
                dgvservicios.ColumnHeadersVisible = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub mostrarproductos()
        Try
            Dim func As New fdetalleproducto
            Dim dts As New vdetalleproducto


            dts.gidventa = txtidventa.Text

            dt = func.mostrar(dts)


            If dt.Rows.Count <> 0 Then
                dgvproductos.DataSource = dt
                dgvproductos.ColumnHeadersVisible = True
                modificar_columnasproductos()
            Else
                dgvproductos.DataSource = Nothing
                dgvproductos.ColumnHeadersVisible = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub buscar()
        Try
            Dim ds As New DataSet
            ds.Tables.Add(dt.Copy)
            Dim dv As New DataView(ds.Tables(0))

            dv.RowFilter = "nombre like '" & txtbuscar.Text & "%'"

            If dv.Count <> 0 Then
                dgvlistado.DataSource = dv
                modificar_columnas()
            Else
                dgvlistado.DataSource = Nothing
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub limpiar()
        txtidcliente.Text = ""
        txtidempleado.Text = ""
        txtidservicio.Text = ""
        txtidproducto.Text = ""
        txtnombrec.Text = ""
        txtapellidoc.Text = ""
        txthora.Text = ""
        txtempleado.Text = ""
        txtservicio.Text = ""
        txtprecios.Text = ""
        txtidservicio.Text = ""
        txtcantidads.Text = "1"
        txtcantidadp.Text = ""
        txtproducto.Text = ""
        txtstock.Text = "1"
        txtpreciop.Text = ""
        txtcantidadp.Text = "1"
        dgvservicios.DataSource = Nothing
        dgvservicios.ColumnHeadersVisible = False
        dgvproductos.DataSource = Nothing
        dgvproductos.ColumnHeadersVisible = False
    End Sub

    Private Sub txtbuscar_TextChanged(sender As Object, e As EventArgs) Handles txtbuscar.TextChanged
        buscar()
    End Sub

    Private Sub btnnuevo_Click(sender As Object, e As EventArgs) Handles btnnuevo.Click
        limpiar()
    End Sub

    Private Sub dgvlistado_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvlistado.CellClick
        limpiar()
        txtidventa.Text = dgvlistado.SelectedCells.Item(0).Value.ToString
        txtidcliente.Text = dgvlistado.SelectedCells.Item(1).Value.ToString
        txtnombrec.Text = dgvlistado.SelectedCells.Item(2).Value.ToString
        txtapellidoc.Text = dgvlistado.SelectedCells.Item(3).Value.ToString
        Dim fecha As String = dgvlistado.SelectedCells.Item(4).Value.ToString
        If Not fecha = "" Then
            dtpfecha.Text = Format(dgvlistado.SelectedCells.Item(4).Value, "dd/MM/yyyy")
        End If
        Dim func As New fventa
        Dim dt2 As New DataTable
        dt2 = func.mostrar(dgvlistado.SelectedCells.Item(0).Value.ToString)
        If dt2.Rows.Count <> 0 Then
            dgvproductos.DataSource = dt2
            dgvproductos.ColumnHeadersVisible = True
            modificar_columnasproductos()
        Else
            dgvproductos.DataSource = Nothing
        End If
        Dim func2 As New fventa
        Dim dt3 As New DataTable
        dt3 = func.mostrarserv(dgvlistado.SelectedCells.Item(0).Value.ToString)
        If dt3.Rows.Count <> 0 Then
            dgvservicios.DataSource = dt3
            dgvservicios.ColumnHeadersVisible = True
            modificar_columnasservicios()
        Else
            dgvservicios.DataSource = Nothing
        End If
    End Sub

    Private Sub btagregars_Click(sender As Object, e As EventArgs) Handles btagregars.Click
        If Me.ValidateChildren = True And txtidventa.Text <> "" And txtempleado.Text <> "" And txtservicio.Text <> "" And txtcantidads.Text <> "" And txtprecios.Text <> "" Then
            Try
                Dim dts As New vdetalleservicio
                Dim func As New fdetalleservicio

                dts.gidventa = txtidventa.Text
                dts.gidservicio = txtidservicio.Text
                dts.gidempleado = txtidempleado.Text
                dts.gcantidad = txtcantidads.Text
                dts.gpreciounitario = txtprecios.Text

                If func.ingresar(dts) Then
                    mostrar()
                    mostrarservicios()
                    txtempleado.Text = ""
                    txtservicio.Text = ""
                    txtprecios.Text = ""
                    txtidservicio.Text = ""
                    txtcantidads.Text = "1"
                    txtidempleado.Text = ""
                    txtidservicio.Text = ""

                Else
                    MessageBox.Show("No se pudo completar el registro", "Guardando Registro", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    mostrarservicios()
                    mostrar()
                    txtempleado.Text = ""
                    txtservicio.Text = ""
                    txtprecios.Text = ""
                    txtidservicio.Text = ""
                    txtcantidads.Text = "1"
                    txtidempleado.Text = ""
                    txtidservicio.Text = ""
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MessageBox.Show("Datos incompletos. Llene los campos obligatorios", "Guardando Registro", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub btagregarp_Click(sender As Object, e As EventArgs) Handles btagregarp.Click
        If Me.ValidateChildren = True And txtidventa.Text <> "" And txtproducto.Text <> "" And txtcantidadp.Text <> "" And txtpreciop.Text <> "" Then
            Try
                Dim dts As New vdetalleproducto
                Dim func As New fdetalleproducto

                dts.gidventa = txtidventa.Text
                dts.gidproducto = txtidproducto.Text
                dts.gcantidad = txtcantidadp.Text
                dts.gpreciounitario = txtpreciop.Text

                If func.ingresar(dts) Then
                    mostrar()
                    mostrarproductos()
                    txtcantidadp.Text = ""
                    txtproducto.Text = ""
                    txtstock.Text = "1"
                    txtpreciop.Text = ""
                    txtcantidadp.Text = "1"
                    txtidproducto.Text = ""
                Else
                    MessageBox.Show("No se pudo completar el registro", "Guardando Registro", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    mostrar()
                    mostrarproductos()
                    txtcantidadp.Text = ""
                    txtproducto.Text = ""
                    txtstock.Text = "1"
                    txtpreciop.Text = ""
                    txtcantidadp.Text = "1"
                    txtidproducto.Text = ""
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MessageBox.Show("Datos incompletos. Llene los campos obligatorios", "Guardando Registro", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        txtnombrec.Text = txthora.Text
    End Sub

    Private Sub btneliminar_Click(sender As Object, e As EventArgs) Handles btneliminar.Click
        Dim result As DialogResult

        result = MessageBox.Show("¿Desea eliminar la venta?", "Modificando Datos", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)

        If result = DialogResult.OK Then

            If Me.ValidateChildren = True And txtnombrec.Text <> "" And txtapellidoc.Text <> "" And txtidventa.Text <> "" Then
                Try
                    Dim dts As New vventa
                    Dim func As New fventa

                    dts.gidventa = txtidventa.Text

                    If func.eliminar(dts) Then
                        MessageBox.Show("Eliminar completado.", "Eliminando Registro", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        mostrar()
                        limpiar()
                        txtidventa.Text = ""
                        ' bloqueartext()
                        'bloquearbtn()
                        btnnuevo.Enabled = True
                    Else
                        MessageBox.Show("No se pudo completar la eliminación.", "Eliminando Registro", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            Else
                MessageBox.Show("Datos incompletos. Llene los campos obligatorios.", "Guardando Registro", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub
End Class