Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DataGridView1.Columns.Add(1, "№ п/п")
        DataGridView1.Columns.Add(1, "Зав.№")
        DataGridView1.Columns.Add(1, "№ ИНТИС")
        DataGridView1.Columns.Add(1, "Год выпуска")
        DataGridView1.Columns.Add(1, "дата поступления в ремонт")
        DataGridView1.Columns.Add(1, "модуль/успд")
        DataGridView1.Columns.Add(1, "неисправность")
        DataGridView1.Columns.Add(1, "причина неисправности, что сделано")
        DataGridView1.Columns.Add(1, "дата ремонта")
        DataGridView1.Columns.Add(1, "пломба")
        DataGridView1.Columns.Add(1, "примечание")
    End Sub
End Class
