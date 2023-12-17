//5.this was autogenerated by a tool. Do not modify! Use partial
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Generated;
[ApiController]
[Route("api/[controller]/[action]")]    
public partial class AdvancedSearch_ApplicationDBContext_TILT_Tag_NoteController : Controller
{
    private ISearchDataTILT_Tag_Note _search;
    public AdvancedSearch_ApplicationDBContext_TILT_Tag_NoteController(ISearchDataTILT_Tag_Note search)
	{
        _search=search;
	}
    [HttpGet]
    public async Task<long> GetAllCount()
    {
       return await _search.GetAllCount();
        
    }
    
    [HttpGet]
    public async IAsyncEnumerable<TILT_Tag_Note_Table> GetAll()
    {
        await foreach(var item in _search.TILT_Tag_NoteFind_AsyncEnumerable(null))
        {
            yield return (TILT_Tag_Note_Table)item!;
        }
        
    }
    [HttpGet]   
    public async IAsyncEnumerable<TILT_Tag_Note_Table> GetSearchSimple(string ColumnName, string Operator, string Value){
           var search = new SearchTILT_Tag_Note();
           search.PageSize = int.MaxValue - 1;
        search.SearchFields = new SearchField<eTILT_Tag_NoteColumns>[1];
        search.SearchFields[0]= new SearchField<eTILT_Tag_NoteColumns>();
        if(Enum.TryParse< eTILT_Tag_NoteColumns >(ColumnName,true ,out var valField)){
            search.SearchFields[0].FieldName = valField;
        }
        else
        {
            search.SearchFields[0].FieldName = (eTILT_Tag_NoteColumns )int.Parse(ColumnName);;
        }
        search.SearchFields[0].Value= Value;
        var criteria= SearchCriteria.None;
        if(Enum.TryParse<SearchCriteria>(Operator,true,out var value))
        {
            criteria = value;
        }
        else
        {
            criteria = (SearchCriteria)int.Parse(Operator);
        }
        
        search.SearchFields[0].Criteria= criteria;
        await foreach (var item in _search.TILT_Tag_NoteFind_AsyncEnumerable(search))
        {
            yield return (TILT_Tag_Note_Table)item!;
        }

    }
    [HttpGet]
    public async IAsyncEnumerable<TILT_Tag_Note_Table> GetWithSearch(SearchTILT_Tag_Note s)
    {
        await foreach(var item in _search.TILT_Tag_NoteFind_AsyncEnumerable(s))
        {
            yield return (TILT_Tag_Note_Table)item!;
        }
        
    }
    [HttpGet]
    public async Task<long> GetWithSearchCount(SearchTILT_Tag_Note? s)
    {
        if (s == null)
            return await GetAllCount();

        return await _search.GetAllCount(s);
    }

//has one key
    [HttpGet]
    public async Task<TILT_Tag_Note_Table?> GetSingle(long id){
        var data=await _search.TILT_Tag_NoteGetSingle(id);
       if(data == null)
        return null;
       return (TILT_Tag_Note_Table)data;
    }

        
    [HttpGet]
    public async IAsyncEnumerable<TILT_Tag_Note_Table> ID_EqualValue( long  value)
    {
        var sc = GeneratorFromDB.SearchCriteria.Equal;
        await foreach (var item in _search.TILT_Tag_NoteSimpleSearch_ID(sc, value))
        {
            yield return (TILT_Tag_Note_Table)item!;
        }
    }
    [HttpGet]
    public async IAsyncEnumerable<TILT_Tag_Note_Table> ID_DifferentValue( long  value)
    {
        var sc = GeneratorFromDB.SearchCriteria.Different;
        await foreach (var item in _search.TILT_Tag_NoteSimpleSearch_ID(sc, value))
        {
            yield return (TILT_Tag_Note_Table)item!;
        }
    }
    [HttpGet]
    public  async IAsyncEnumerable<TILT_Tag_Note_Table> ID_SimpleSearch(GeneratorFromDB.SearchCriteria sc,  long value){
        await foreach(var item in _search.TILT_Tag_NoteSimpleSearch_ID(sc,value))
        {
            yield return (TILT_Tag_Note_Table)item!;
        }
    }

         
     [HttpGet]
    public async IAsyncEnumerable<TILT_Tag_Note_Table> ID_EqualValues( long[]  values)
    {
        string? value=null;
        if(values.Length>0)
            value=string.Join( ",",values);
        var sc=SearchTILT_Tag_Note.FromSearch(GeneratorFromDB.SearchCriteria.InArray,eTILT_Tag_NoteColumns.ID,value);
        await foreach (var item in _search.TILT_Tag_NoteFind_AsyncEnumerable(sc))
        {
        
            yield return (TILT_Tag_Note_Table)item!;
        }
    }
     [HttpGet]
    public async IAsyncEnumerable<TILT_Tag_Note_Table> ID_DifferentValues( long[]  values)
    {
        string? value=null;
        if(values.Length>0)
            value=string.Join( ",",values);
        var sc=SearchTILT_Tag_Note.FromSearch(GeneratorFromDB.SearchCriteria.NotInArray,eTILT_Tag_NoteColumns.ID,value);
        await foreach (var item in _search.TILT_Tag_NoteFind_AsyncEnumerable(sc))
        {
        
            yield return (TILT_Tag_Note_Table)item!;
        }
    }
              [HttpGet]
    public async IAsyncEnumerable<TILT_Tag_Note_Table> ID_LessOrEqual(long  val)
    {
        var sc = SearchTILT_Tag_Note.FromSearch(GeneratorFromDB.SearchCriteria.LessOrEqual, eTILT_Tag_NoteColumns.ID  , val.ToString());
        await foreach (var item in _search.TILT_Tag_NoteFind_AsyncEnumerable(sc))
        {

            yield return (TILT_Tag_Note_Table)item!;
        }
    }
     [HttpGet]
    public async IAsyncEnumerable<TILT_Tag_Note_Table> ID_Less(long  val)
    {
        var sc = SearchTILT_Tag_Note.FromSearch(GeneratorFromDB.SearchCriteria.Less, eTILT_Tag_NoteColumns.ID  , val.ToString());
        await foreach (var item in _search.TILT_Tag_NoteFind_AsyncEnumerable(sc))
        {

            yield return (TILT_Tag_Note_Table)item!;
        }
    }
     
      [HttpGet]
    public async IAsyncEnumerable<TILT_Tag_Note_Table> ID_GreaterOrEqual(long  val)
    {
        var sc = SearchTILT_Tag_Note.FromSearch(GeneratorFromDB.SearchCriteria.GreaterOrEqual, eTILT_Tag_NoteColumns.ID  , val.ToString());
        await foreach (var item in _search.TILT_Tag_NoteFind_AsyncEnumerable(sc))
        {

            yield return (TILT_Tag_Note_Table)item!;
        }
    }
     [HttpGet]
    public async IAsyncEnumerable<TILT_Tag_Note_Table> ID_Greater(long  val)
    {
        var sc = SearchTILT_Tag_Note.FromSearch(GeneratorFromDB.SearchCriteria.Greater, eTILT_Tag_NoteColumns.ID  , val.ToString());
        await foreach (var item in _search.TILT_Tag_NoteFind_AsyncEnumerable(sc))
        {

            yield return (TILT_Tag_Note_Table)item!;
        }
    }
     [HttpGet]
    public async IAsyncEnumerable<TILT_Tag_Note_Table> ID_Between( long  valStart, long valEnd)
    {
        var sc = SearchTILT_Tag_Note.FromSearch(GeneratorFromDB.SearchCriteria.Between, eTILT_Tag_NoteColumns.ID, valStart +","+ valEnd);
        await foreach (var item in _search.TILT_Tag_NoteFind_AsyncEnumerable(sc))
        {

            yield return (TILT_Tag_Note_Table)item!;
        }
    }    

    [HttpGet]
    public async IAsyncEnumerable<TILT_Tag_Note_Table> ID_NotBetween( long  valStart, long valEnd)
    {
        var sc = SearchTILT_Tag_Note.FromSearch(GeneratorFromDB.SearchCriteria.NotBetween, eTILT_Tag_NoteColumns.ID, valStart +","+ valEnd);
        await foreach (var item in _search.TILT_Tag_NoteFind_AsyncEnumerable(sc))
        {

            yield return (TILT_Tag_Note_Table)item!;
        }
    }    

        
    [HttpGet]
    public async IAsyncEnumerable<TILT_Tag_Note_Table> IDTag_EqualValue( long  value)
    {
        var sc = GeneratorFromDB.SearchCriteria.Equal;
        await foreach (var item in _search.TILT_Tag_NoteSimpleSearch_IDTag(sc, value))
        {
            yield return (TILT_Tag_Note_Table)item!;
        }
    }
    [HttpGet]
    public async IAsyncEnumerable<TILT_Tag_Note_Table> IDTag_DifferentValue( long  value)
    {
        var sc = GeneratorFromDB.SearchCriteria.Different;
        await foreach (var item in _search.TILT_Tag_NoteSimpleSearch_IDTag(sc, value))
        {
            yield return (TILT_Tag_Note_Table)item!;
        }
    }
    [HttpGet]
    public  async IAsyncEnumerable<TILT_Tag_Note_Table> IDTag_SimpleSearch(GeneratorFromDB.SearchCriteria sc,  long value){
        await foreach(var item in _search.TILT_Tag_NoteSimpleSearch_IDTag(sc,value))
        {
            yield return (TILT_Tag_Note_Table)item!;
        }
    }

         
     [HttpGet]
    public async IAsyncEnumerable<TILT_Tag_Note_Table> IDTag_EqualValues( long[]  values)
    {
        string? value=null;
        if(values.Length>0)
            value=string.Join( ",",values);
        var sc=SearchTILT_Tag_Note.FromSearch(GeneratorFromDB.SearchCriteria.InArray,eTILT_Tag_NoteColumns.IDTag,value);
        await foreach (var item in _search.TILT_Tag_NoteFind_AsyncEnumerable(sc))
        {
        
            yield return (TILT_Tag_Note_Table)item!;
        }
    }
     [HttpGet]
    public async IAsyncEnumerable<TILT_Tag_Note_Table> IDTag_DifferentValues( long[]  values)
    {
        string? value=null;
        if(values.Length>0)
            value=string.Join( ",",values);
        var sc=SearchTILT_Tag_Note.FromSearch(GeneratorFromDB.SearchCriteria.NotInArray,eTILT_Tag_NoteColumns.IDTag,value);
        await foreach (var item in _search.TILT_Tag_NoteFind_AsyncEnumerable(sc))
        {
        
            yield return (TILT_Tag_Note_Table)item!;
        }
    }
              [HttpGet]
    public async IAsyncEnumerable<TILT_Tag_Note_Table> IDTag_LessOrEqual(long  val)
    {
        var sc = SearchTILT_Tag_Note.FromSearch(GeneratorFromDB.SearchCriteria.LessOrEqual, eTILT_Tag_NoteColumns.IDTag  , val.ToString());
        await foreach (var item in _search.TILT_Tag_NoteFind_AsyncEnumerable(sc))
        {

            yield return (TILT_Tag_Note_Table)item!;
        }
    }
     [HttpGet]
    public async IAsyncEnumerable<TILT_Tag_Note_Table> IDTag_Less(long  val)
    {
        var sc = SearchTILT_Tag_Note.FromSearch(GeneratorFromDB.SearchCriteria.Less, eTILT_Tag_NoteColumns.IDTag  , val.ToString());
        await foreach (var item in _search.TILT_Tag_NoteFind_AsyncEnumerable(sc))
        {

            yield return (TILT_Tag_Note_Table)item!;
        }
    }
     
      [HttpGet]
    public async IAsyncEnumerable<TILT_Tag_Note_Table> IDTag_GreaterOrEqual(long  val)
    {
        var sc = SearchTILT_Tag_Note.FromSearch(GeneratorFromDB.SearchCriteria.GreaterOrEqual, eTILT_Tag_NoteColumns.IDTag  , val.ToString());
        await foreach (var item in _search.TILT_Tag_NoteFind_AsyncEnumerable(sc))
        {

            yield return (TILT_Tag_Note_Table)item!;
        }
    }
     [HttpGet]
    public async IAsyncEnumerable<TILT_Tag_Note_Table> IDTag_Greater(long  val)
    {
        var sc = SearchTILT_Tag_Note.FromSearch(GeneratorFromDB.SearchCriteria.Greater, eTILT_Tag_NoteColumns.IDTag  , val.ToString());
        await foreach (var item in _search.TILT_Tag_NoteFind_AsyncEnumerable(sc))
        {

            yield return (TILT_Tag_Note_Table)item!;
        }
    }
     [HttpGet]
    public async IAsyncEnumerable<TILT_Tag_Note_Table> IDTag_Between( long  valStart, long valEnd)
    {
        var sc = SearchTILT_Tag_Note.FromSearch(GeneratorFromDB.SearchCriteria.Between, eTILT_Tag_NoteColumns.IDTag, valStart +","+ valEnd);
        await foreach (var item in _search.TILT_Tag_NoteFind_AsyncEnumerable(sc))
        {

            yield return (TILT_Tag_Note_Table)item!;
        }
    }    

    [HttpGet]
    public async IAsyncEnumerable<TILT_Tag_Note_Table> IDTag_NotBetween( long  valStart, long valEnd)
    {
        var sc = SearchTILT_Tag_Note.FromSearch(GeneratorFromDB.SearchCriteria.NotBetween, eTILT_Tag_NoteColumns.IDTag, valStart +","+ valEnd);
        await foreach (var item in _search.TILT_Tag_NoteFind_AsyncEnumerable(sc))
        {

            yield return (TILT_Tag_Note_Table)item!;
        }
    }    

        
    [HttpGet]
    public async IAsyncEnumerable<TILT_Tag_Note_Table> IDNote_EqualValue( long  value)
    {
        var sc = GeneratorFromDB.SearchCriteria.Equal;
        await foreach (var item in _search.TILT_Tag_NoteSimpleSearch_IDNote(sc, value))
        {
            yield return (TILT_Tag_Note_Table)item!;
        }
    }
    [HttpGet]
    public async IAsyncEnumerable<TILT_Tag_Note_Table> IDNote_DifferentValue( long  value)
    {
        var sc = GeneratorFromDB.SearchCriteria.Different;
        await foreach (var item in _search.TILT_Tag_NoteSimpleSearch_IDNote(sc, value))
        {
            yield return (TILT_Tag_Note_Table)item!;
        }
    }
    [HttpGet]
    public  async IAsyncEnumerable<TILT_Tag_Note_Table> IDNote_SimpleSearch(GeneratorFromDB.SearchCriteria sc,  long value){
        await foreach(var item in _search.TILT_Tag_NoteSimpleSearch_IDNote(sc,value))
        {
            yield return (TILT_Tag_Note_Table)item!;
        }
    }

         
     [HttpGet]
    public async IAsyncEnumerable<TILT_Tag_Note_Table> IDNote_EqualValues( long[]  values)
    {
        string? value=null;
        if(values.Length>0)
            value=string.Join( ",",values);
        var sc=SearchTILT_Tag_Note.FromSearch(GeneratorFromDB.SearchCriteria.InArray,eTILT_Tag_NoteColumns.IDNote,value);
        await foreach (var item in _search.TILT_Tag_NoteFind_AsyncEnumerable(sc))
        {
        
            yield return (TILT_Tag_Note_Table)item!;
        }
    }
     [HttpGet]
    public async IAsyncEnumerable<TILT_Tag_Note_Table> IDNote_DifferentValues( long[]  values)
    {
        string? value=null;
        if(values.Length>0)
            value=string.Join( ",",values);
        var sc=SearchTILT_Tag_Note.FromSearch(GeneratorFromDB.SearchCriteria.NotInArray,eTILT_Tag_NoteColumns.IDNote,value);
        await foreach (var item in _search.TILT_Tag_NoteFind_AsyncEnumerable(sc))
        {
        
            yield return (TILT_Tag_Note_Table)item!;
        }
    }
              [HttpGet]
    public async IAsyncEnumerable<TILT_Tag_Note_Table> IDNote_LessOrEqual(long  val)
    {
        var sc = SearchTILT_Tag_Note.FromSearch(GeneratorFromDB.SearchCriteria.LessOrEqual, eTILT_Tag_NoteColumns.IDNote  , val.ToString());
        await foreach (var item in _search.TILT_Tag_NoteFind_AsyncEnumerable(sc))
        {

            yield return (TILT_Tag_Note_Table)item!;
        }
    }
     [HttpGet]
    public async IAsyncEnumerable<TILT_Tag_Note_Table> IDNote_Less(long  val)
    {
        var sc = SearchTILT_Tag_Note.FromSearch(GeneratorFromDB.SearchCriteria.Less, eTILT_Tag_NoteColumns.IDNote  , val.ToString());
        await foreach (var item in _search.TILT_Tag_NoteFind_AsyncEnumerable(sc))
        {

            yield return (TILT_Tag_Note_Table)item!;
        }
    }
     
      [HttpGet]
    public async IAsyncEnumerable<TILT_Tag_Note_Table> IDNote_GreaterOrEqual(long  val)
    {
        var sc = SearchTILT_Tag_Note.FromSearch(GeneratorFromDB.SearchCriteria.GreaterOrEqual, eTILT_Tag_NoteColumns.IDNote  , val.ToString());
        await foreach (var item in _search.TILT_Tag_NoteFind_AsyncEnumerable(sc))
        {

            yield return (TILT_Tag_Note_Table)item!;
        }
    }
     [HttpGet]
    public async IAsyncEnumerable<TILT_Tag_Note_Table> IDNote_Greater(long  val)
    {
        var sc = SearchTILT_Tag_Note.FromSearch(GeneratorFromDB.SearchCriteria.Greater, eTILT_Tag_NoteColumns.IDNote  , val.ToString());
        await foreach (var item in _search.TILT_Tag_NoteFind_AsyncEnumerable(sc))
        {

            yield return (TILT_Tag_Note_Table)item!;
        }
    }
     [HttpGet]
    public async IAsyncEnumerable<TILT_Tag_Note_Table> IDNote_Between( long  valStart, long valEnd)
    {
        var sc = SearchTILT_Tag_Note.FromSearch(GeneratorFromDB.SearchCriteria.Between, eTILT_Tag_NoteColumns.IDNote, valStart +","+ valEnd);
        await foreach (var item in _search.TILT_Tag_NoteFind_AsyncEnumerable(sc))
        {

            yield return (TILT_Tag_Note_Table)item!;
        }
    }    

    [HttpGet]
    public async IAsyncEnumerable<TILT_Tag_Note_Table> IDNote_NotBetween( long  valStart, long valEnd)
    {
        var sc = SearchTILT_Tag_Note.FromSearch(GeneratorFromDB.SearchCriteria.NotBetween, eTILT_Tag_NoteColumns.IDNote, valStart +","+ valEnd);
        await foreach (var item in _search.TILT_Tag_NoteFind_AsyncEnumerable(sc))
        {

            yield return (TILT_Tag_Note_Table)item!;
        }
    }    

            


    


}//end class

