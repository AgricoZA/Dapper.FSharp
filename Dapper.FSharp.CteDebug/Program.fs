// Learn more about F# at http://fsharp.org

open System

open Dapper.FSharp
open Dapper.FSharp.MSSQL

type dbRecord = {
    id : string
    field1 : int
}

type dbRecord2 = {
    id1 : string
    field3 : int
}

let dbRecordL = [{id = "1"; field1 = 1}; {id = "2"; field1 = 2}]
[<EntryPoint>]
let main argv =
    let CTEQuery = CTESelect {
        table "dbo.TestTable"
        where (eq "id" 5)
    }

    let selectWithCTE = select {
        withCTE {AsTableName = "MyTableName"; CTESelectQuery =  CTEQuery  }
        table "dbo.TestTable"
        innerJoin "subQuery" "id" "dbo.TestTable.id"
        where (eq "id" 1)
    }

    let insertq = insert {
        table "myTable"
        values dbRecordL
    }

    printfn "cte select: %A" CTEQuery
    printfn "select query: %A" selectWithCTE
    printfn "%A" (Deconstructor.select<dbRecord, dbRecord2> selectWithCTE )
    printfn "%A" (Deconstructor.insert<dbRecord> insertq)
    0 // return an integer exit code
