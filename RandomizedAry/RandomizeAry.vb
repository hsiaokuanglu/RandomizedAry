Imports System.IO

Public Class DataAry
    Protected Width As Integer
    Protected Height As Integer
    Protected FileName As String 'file location
    'array store pairs of x, y coordinates
    Private DataAry(Width * Height - 1) As Pair
    ''Set and get file location
    ''' <summary>
    ''' A string of the complete file location to write to 
    ''' </summary>
    ''' <returns>A string of the current file location</returns>
    Public Property SetNewFileLocation() As String
        Get
            Return FileName
        End Get
        Set(ByVal value As String)
            FileName = value
        End Set
    End Property
    Public Sub New(_width As Integer, _height As Integer, _fileNm As String)
        Width = _width
        Height = _height
        FileName = _fileNm
        DataAry = New Pair(Width * Height - 1) {}
        'initialize Array
        For a As Integer = 1 To Width
            For b As Integer = 1 To Height
                DataAry((a - 1) * Height + b - 1) = New Pair(a, b)
            Next
        Next
    End Sub

    ''' <summary>
    ''' 混 Bin
    ''' </summary>
    'Randomize the Array
    Public Sub Randomize()
        'Shuffle array
        Dim rnd As New Random()
        Shuffle(Of Pair)(DataAry, rnd)
    End Sub

    ''' <summary>
    ''' Write file to the file location
    ''' </summary>
    'Write the Arrya to the file location 
    Public Sub WriteAry()
        Using writer As StreamWriter = New StreamWriter(FileName)
            For i As Integer = 0 To Height * Width - 1
                writer.WriteLine($"{DataAry(i).GetA},{DataAry(i).GetB},1")
            Next
        End Using

    End Sub

    ''' <summary>
    ''' A pair of x, y coordinate
    ''' </summary>
    ''Pairs of x, y Coordinates
    Private Class Pair
        Protected A As Integer
        Protected B As Integer

        Public Sub New(ByVal _a As Integer, ByVal _b As Integer)
            A = _a
            B = _b
        End Sub
        ReadOnly Property GetA() As Integer
            Get
                Return A
            End Get
        End Property
        ReadOnly Property GetB() As Integer
            Get
                Return B
            End Get
        End Property
    End Class
    'Shuffle 
    Protected Sub Shuffle(Of T)(items As T(), rng As Random)
        Dim temp As T
        Dim j As Int32
        For i As Int32 = items.Count - 1 To 0 Step -1
            ' Pick an item for position i.
            j = rng.Next(i + 1)
            ' Swap 
            temp = items(i)
            items(i) = items(j)
            items(j) = temp
        Next i
    End Sub
End Class

Public Class TestDataAry
    Inherits DataAry
    Private TestRndAry()
    Private Types As Integer

    Public Sub New(_width As Integer, _height As Integer, _fileNm As String, _types As Integer)
        MyBase.New(_width, _height, _fileNm)
        Types = _types
        TestRndAry = New Tuple(Width * Height - 1) {}
        'initalize testAry
        For a As Integer = 1 To Width
            For b As Integer = 1 To Height
                TestRndAry((a - 1) * Height + b - 1) = New Tuple(a, b)
            Next
        Next
        Dim numOfItem = Width * Height / Types
        For g As Integer = 0 To Types - 1
            For n As Integer = 0 To numOfItem - 1
                TestRndAry(n + (numOfItem * g)).SetD(g + 1)
            Next
        Next
    End Sub

    Public Sub RandomizeTestAry()
        Dim rnd As New Random()
        Shuffle(Of Tuple)(TestRndAry, rnd)
    End Sub

    Public Sub WriteTestAry()
        Using writer As System.IO.StreamWriter = New System.IO.StreamWriter(FileName)
            For i As Integer = 0 To Height * Width - 1
                writer.WriteLine($"{TestRndAry(i).A},{TestRndAry(i).B},1,{TestRndAry(i).D}")
            Next
        End Using
    End Sub

    Private Class Tuple
        Public A As Integer
        Public B As Integer
        Public C As Integer
        Public D As Integer
        Public neighbor() As Tuple
        Public Sub New(ByVal _a As Integer, ByVal _b As Integer)
            A = _a
            B = _b
        End Sub
        Public Sub LinkNeighbor(ByRef neighborT As Tuple, ByVal index As Integer)
            neighbor(index) = neighborT
        End Sub
        Public Sub SetD(ByVal _d As Integer)
            D = _d
        End Sub
    End Class
End Class

