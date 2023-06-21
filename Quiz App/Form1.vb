Public Class Form1
    Private TargetDT As DateTime
    Private CountDownFrom As TimeSpan = TimeSpan.FromMinutes(2)


    Public CurrQuestionIndex = 0
    Dim QAMatrix2D As String(,) = {
        {"Satu + Satu adalah...", "Dua"},
        {"Dua + Dua = adalah...", "Empat"},
        {"Tiga + Tiga adalah...", "Enam"},
        {"Empat + Empat adalah...", "Delapan"},
        {"Lima + Lima adalah...", "Sepuluh"},
        {"Sepuluh + Dua adalah...", "Dua Belas"}}
    Dim DUMMYAMatrix3D As String(,,) = {
        {{"Dua", "Tiga", "Empat"}},
        {{"Satu", "Empat", "Tiga"}},
        {{"Delapan", "Tiga", "Enam"}},
        {{"Dua Puluh", "Tiga", "Delapan"}},
        {{"Sepuluh", "Tiga Belas", "Dua Ratus"}},
        {{"Empat Puluh Dua", "Lima Puluh Lima", "Dua Belas"}}}
    Public RBMatrix1D As New List(Of RadioButton)()


    Public selectedAns As String
    Public selectedAnsArrayStorage As String()


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ReDim selectedAnsArrayStorage(QAMatrix2D.Length)

        Button2.Text = "Prev"
        Button3.Text = "Next"
        Timer2.Start()

        Label2.Text = String.Format("Question {0}:", CurrQuestionIndex + 1)
        TextBox2.Text = QAMatrix2D(0, 0)

        RBMatrix1D.Add(RadioButton1)
        RBMatrix1D.Add(RadioButton2)
        RBMatrix1D.Add(RadioButton3)

        'MsgBox(DUMMYAMatrix3D(0, 0, 0))    'DUA
        'MsgBox(DUMMYAMatrix3D(0, 0, 1))     'TIGA
        'MsgBox(DUMMYAMatrix3D(0, 0, 2))     'EMPAT

        For RBIndex As Integer = 0 To (2)
            RBMatrix1D(RBIndex).Text = DUMMYAMatrix3D(0, 0, RBIndex)
        Next
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        cekField()
    End Sub


    Private Sub TextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If (e.KeyChar = Chr(13)) Then
            ComboBox1.Focus()
            ComboBox1.DroppedDown = True
        End If
    End Sub
    Private Sub ComboBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox1.KeyPress
        If (e.KeyChar = Chr(13)) Then
            cekField()
        End If
    End Sub


    Private Sub cekField()
        If (TextBox1.Text <> "") Then
            If (ComboBox1.Text <> "") Then
                TableLayoutPanel3.Visible = False
                TableLayoutPanel1.Visible = True

                TargetDT = DateTime.Now.Add(CountDownFrom)
                Timer1.Start()

            Else
                MsgBox("Prodi tidak boleh kosong !", , "Peringatan")
                ComboBox1.Focus()
                ComboBox1.DroppedDown = True

            End If

        Else
            MsgBox("Nama tidak boleh kosong !", , "Peringatan")
            TextBox1.Focus()

        End If
    End Sub


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim ts As TimeSpan = TargetDT.Subtract(DateTime.Now)
        If ts.TotalMilliseconds > 0 Then
            Label1.Text = ts.ToString("mm\:ss")
        Else
            Label1.Text = "00:00"
            Timer1.Stop()

            For XYZ As Integer = 0 To (QAMatrix2D.Length / 2 - 1)
                If (QAMatrix2D(XYZ, 1) = selectedAnsArrayStorage(XYZ)) Then
                    Form2.Label6.Text = Val(Form2.Label6.Text) + 10
                End If
            Next

            Button3.Text = "Next"
            Me.Hide()
            Form2.Show()
            CurrQuestionIndex = 0
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If (Button3.Text = "Submit") Then
            Button3.Text = "Next"
        End If

        If ((CurrQuestionIndex + 1) <> 1) Then
            CurrQuestionIndex -= 1

            TextBox2.Text = QAMatrix2D(CurrQuestionIndex, 0)
            For RBIndex As Integer = 0 To (2)
                RBMatrix1D(RBIndex).Text = DUMMYAMatrix3D(CurrQuestionIndex, 0, RBIndex)
                If (selectedAnsArrayStorage(CurrQuestionIndex) <> "") Then
                    If (selectedAnsArrayStorage(CurrQuestionIndex) = RBMatrix1D(RBIndex).Text) Then
                        RBMatrix1D(RBIndex).Select()
                    End If
                Else
                    RBMatrix1D(RBIndex).Checked = False
                End If
            Next

        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If (Button3.Text = "Submit") Then
            Label1.Text = "00:00"
            Timer1.Stop()

            For XYZ As Integer = 0 To (QAMatrix2D.Length / 2 - 1)
                If (QAMatrix2D(XYZ, 1) = selectedAnsArrayStorage(XYZ)) Then
                    Form2.Label6.Text = Val(Form2.Label6.Text) + 10
                End If
            Next

            Button3.Text = "Next"
            Me.Hide()
            Form2.Show()
            CurrQuestionIndex = 0
        Else
            If ((CurrQuestionIndex + 1) < QAMatrix2D.Length / 2) Then
                CurrQuestionIndex += 1

                TextBox2.Text = QAMatrix2D(CurrQuestionIndex, 0)
                For RBIndex As Integer = 0 To (2)
                    RBMatrix1D(RBIndex).Text = DUMMYAMatrix3D(CurrQuestionIndex, 0, RBIndex)

                    If (selectedAnsArrayStorage(CurrQuestionIndex) <> "") Then
                        If (selectedAnsArrayStorage(CurrQuestionIndex) = RBMatrix1D(RBIndex).Text) Then
                            RBMatrix1D(RBIndex).Select()
                        End If
                    Else
                        RBMatrix1D(RBIndex).Checked = False
                    End If
                Next

            End If
        End If

    End Sub


    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Label2.Text = String.Format("Question {0}:", CurrQuestionIndex + 1)

        If ((CurrQuestionIndex + 1) = QAMatrix2D.Length / 2) Then
            Button3.Text = "Submit"
        End If

        If ((CurrQuestionIndex + 1) = 1) Then
            Button2.Enabled = False
        Else
            Button2.Enabled = True
        End If


        If selectedAnsArrayStorage.Count <> 0 Then
            If (selectedAnsArrayStorage(CurrQuestionIndex) <> "") Then
                Label8.Text = String.Format("Locked Answer ({0})", selectedAnsArrayStorage(CurrQuestionIndex))
            Else
                Label8.Text = "Locked Answer (None)"
            End If

        End If

    End Sub


    Private Sub RadioButton1_Click(sender As Object, e As EventArgs) Handles RadioButton1.Click
        selectedAns = RadioButton1.Text
        selectedAnsArrayStorage(CurrQuestionIndex) = selectedAns
    End Sub

    Private Sub RadioButton2_Click(sender As Object, e As EventArgs) Handles RadioButton2.Click
        selectedAns = RadioButton2.Text
        selectedAnsArrayStorage(CurrQuestionIndex) = selectedAns
    End Sub

    Private Sub RadioButton3_Click(sender As Object, e As EventArgs) Handles RadioButton3.Click
        selectedAns = RadioButton3.Text
        selectedAnsArrayStorage(CurrQuestionIndex) = selectedAns

    End Sub


End Class
