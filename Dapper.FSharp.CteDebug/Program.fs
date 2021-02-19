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


[<EntryPoint>]
let main argv =
    let subQuery = select {
        table "dbo.TestTable"
        where (eq "id" 5)
    }

    let selectWithCTE = select {
        withCTE (cte "subQuery" subQuery)
        table "dbo.TestTable"
        innerJoin "subQuery" "id" "dbo.TestTable.id"
        where (eq "id" 1)
    }

    printfn "select query: %A" selectWithCTE
    printfn "%A" ( Deconstructor.select<dbRecord, dbRecord2> selectWithCTE )
    0 // return an integer exit code
