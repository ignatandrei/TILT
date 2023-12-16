﻿//1.this was autogenerated by a tool. Do not modify! Use partial
using System;
using System.Collections.Generic;
using GeneratorFromDB;
using Microsoft.EntityFrameworkCore;

//modified 2023.12.15
namespace Generated;

public partial class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TILT_Note> TILT_Note { get; set; }
    //public virtual DbSet<TILT_Note_Table> TILT_Note_Table { get; set; }

    public virtual DbSet<TILT_Tag> TILT_Tag { get; set; }
    //public virtual DbSet<TILT_Tag_Table> TILT_Tag_Table { get; set; }

    public virtual DbSet<TILT_Tag_Note> TILT_Tag_Note { get; set; }
    //public virtual DbSet<TILT_Tag_Note_Table> TILT_Tag_Note_Table { get; set; }

    public virtual DbSet<TILT_URL> TILT_URL { get; set; }
    //public virtual DbSet<TILT_URL_Table> TILT_URL_Table { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

//added new
public partial class ApplicationDBContext : DbContext
{
    public static MetaDB  metaData = new("ApplicationDBContext");
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void ApplicationDBContext_AddTables(){
        metaData.AddTable(TILT_Note_Table.metaData);
            metaData.AddTable(TILT_Tag_Table.metaData);
            metaData.AddTable(TILT_Tag_Note_Table.metaData);
            metaData.AddTable(TILT_URL_Table.metaData);
        AllDB.Singleton.AddDb(metaData);
    }

        public async Task<TILT_Note[]> TILT_NoteFind_Array( SearchTILT_Note? search){
 
        IQueryable<TILT_Note> data= this.TILT_Note ;
        if(search == null){
            return await data.ToArrayAsync();
        }
        data = search.TransformToWhere(data);        
        IQueryable<TILT_Note> ret= search.TransformToOrder(data);
        if(search.PageNumber>1)
            ret= ret.Skip((search.PageNumber-1)*search.PageSize);
            
        ret=ret.Take(search.PageSize);
        return await ret.ToArrayAsync();
    }

    public  IAsyncEnumerable<TILT_Note> TILT_NoteGetAll(){
        return this.TILT_NoteFind_AsyncEnumerable(null);
    }
        public async  Task<TILT_Note> TILT_NoteSave(TILT_Note val){

        var data= await TILT_NoteSaveMultiple(val);
        if(data == null)
            return null;

        return data[0];
                
    }
    public async  Task<TILT_Note[]> TILT_NoteSaveMultiple(params TILT_Note[] values){

        if(values == null)
            return null;
        if(values.Length == 0)
            return values;
        foreach(var value in values){
            this.TILT_Note.Add(value);
        }
        await SaveChangesAsync();
        return values;

                
    }

    public  IAsyncEnumerable<TILT_Note> TILT_NoteSimpleSearch(GeneratorFromDB.SearchCriteria sc, eTILT_NoteColumns colToSearch, string value){
        
 var search = SearchTILT_Note.FromSearch(sc,colToSearch,value);
 /*
        var orderBy = new GeneratorFromDB.OrderBy<eTILT_NoteColumns>();
                    orderBy.FieldName = colToSearch;
                orderBy.Asc = false;
        search.OrderBys = new[] { orderBy };
        search.PageNumber = 1;
        search.PageSize = int.MaxValue-1;
        var s = new GeneratorFromDB.SearchField<eTILT_NoteColumns>();
        s.Criteria = sc;
        s.FieldName = colToSearch;
        s.Value = value;
        search.SearchFields = new[] { s };
        */
        var data = this.TILT_NoteFind_AsyncEnumerable(search);
        return data;
    }   
/*
    public IAsyncEnumerable<TILT_Note> TILT_NoteFind_AsyncEnumerable(SearchTILT_Note? search){
 
        IQueryable<TILT_Note> data= this.TILT_Note ;
        if(search == null){
            return data.AsAsyncEnumerable();
        }
        data = search.TransformToWhere(data);        
        IQueryable<TILT_Note> =search.TransformToOrder(data);
        if(search.PageSize>1)
            ret= ret.Skip((search.PageNumber-1)*search.PageSize);
            
        ret=ret.Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    }
*/
    public Task<long> TILT_NoteCount( SearchTILT_Note? search){
     
        IQueryable<TILT_Note> data= this.TILT_Note ;
        if(search != null){
            data = search.TransformToWhere(data);        
        }        
        return data.LongCountAsync();
    }
    

    

    public IAsyncEnumerable<TILT_Note> TILT_NoteFind_AsyncEnumerable(SearchTILT_Note? search){
        IQueryable<TILT_Note> data= this.TILT_Note ;
        if(search == null){
                        return data.AsAsyncEnumerable();
                    }
        data = search.TransformToWhere(data); 
        
        IQueryable<TILT_Note> ret= search.TransformToOrder(data);
        if(search.PageNumber>1)
            ret=ret.Skip((search.PageNumber-1)*search.PageSize);
            
        ret=ret.Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    
    }

        public async Task<TILT_Tag[]> TILT_TagFind_Array( SearchTILT_Tag? search){
 
        IQueryable<TILT_Tag> data= this.TILT_Tag ;
        if(search == null){
            return await data.ToArrayAsync();
        }
        data = search.TransformToWhere(data);        
        IQueryable<TILT_Tag> ret= search.TransformToOrder(data);
        if(search.PageNumber>1)
            ret= ret.Skip((search.PageNumber-1)*search.PageSize);
            
        ret=ret.Take(search.PageSize);
        return await ret.ToArrayAsync();
    }

    public  IAsyncEnumerable<TILT_Tag> TILT_TagGetAll(){
        return this.TILT_TagFind_AsyncEnumerable(null);
    }
        public async  Task<TILT_Tag> TILT_TagSave(TILT_Tag val){

        var data= await TILT_TagSaveMultiple(val);
        if(data == null)
            return null;

        return data[0];
                
    }
    public async  Task<TILT_Tag[]> TILT_TagSaveMultiple(params TILT_Tag[] values){

        if(values == null)
            return null;
        if(values.Length == 0)
            return values;
        foreach(var value in values){
            this.TILT_Tag.Add(value);
        }
        await SaveChangesAsync();
        return values;

                
    }

    public  IAsyncEnumerable<TILT_Tag> TILT_TagSimpleSearch(GeneratorFromDB.SearchCriteria sc, eTILT_TagColumns colToSearch, string value){
        
 var search = SearchTILT_Tag.FromSearch(sc,colToSearch,value);
 /*
        var orderBy = new GeneratorFromDB.OrderBy<eTILT_TagColumns>();
                    orderBy.FieldName = colToSearch;
                orderBy.Asc = false;
        search.OrderBys = new[] { orderBy };
        search.PageNumber = 1;
        search.PageSize = int.MaxValue-1;
        var s = new GeneratorFromDB.SearchField<eTILT_TagColumns>();
        s.Criteria = sc;
        s.FieldName = colToSearch;
        s.Value = value;
        search.SearchFields = new[] { s };
        */
        var data = this.TILT_TagFind_AsyncEnumerable(search);
        return data;
    }   
/*
    public IAsyncEnumerable<TILT_Tag> TILT_TagFind_AsyncEnumerable(SearchTILT_Tag? search){
 
        IQueryable<TILT_Tag> data= this.TILT_Tag ;
        if(search == null){
            return data.AsAsyncEnumerable();
        }
        data = search.TransformToWhere(data);        
        IQueryable<TILT_Tag> =search.TransformToOrder(data);
        if(search.PageSize>1)
            ret= ret.Skip((search.PageNumber-1)*search.PageSize);
            
        ret=ret.Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    }
*/
    public Task<long> TILT_TagCount( SearchTILT_Tag? search){
     
        IQueryable<TILT_Tag> data= this.TILT_Tag ;
        if(search != null){
            data = search.TransformToWhere(data);        
        }        
        return data.LongCountAsync();
    }
    

    

    public IAsyncEnumerable<TILT_Tag> TILT_TagFind_AsyncEnumerable(SearchTILT_Tag? search){
        IQueryable<TILT_Tag> data= this.TILT_Tag ;
        if(search == null){
                        return data.AsAsyncEnumerable();
                    }
        data = search.TransformToWhere(data); 
        
        IQueryable<TILT_Tag> ret= search.TransformToOrder(data);
        if(search.PageNumber>1)
            ret=ret.Skip((search.PageNumber-1)*search.PageSize);
            
        ret=ret.Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    
    }

        public async Task<TILT_Tag_Note[]> TILT_Tag_NoteFind_Array( SearchTILT_Tag_Note? search){
 
        IQueryable<TILT_Tag_Note> data= this.TILT_Tag_Note ;
        if(search == null){
            return await data.ToArrayAsync();
        }
        data = search.TransformToWhere(data);        
        IQueryable<TILT_Tag_Note> ret= search.TransformToOrder(data);
        if(search.PageNumber>1)
            ret= ret.Skip((search.PageNumber-1)*search.PageSize);
            
        ret=ret.Take(search.PageSize);
        return await ret.ToArrayAsync();
    }

    public  IAsyncEnumerable<TILT_Tag_Note> TILT_Tag_NoteGetAll(){
        return this.TILT_Tag_NoteFind_AsyncEnumerable(null);
    }
        public async  Task<TILT_Tag_Note> TILT_Tag_NoteSave(TILT_Tag_Note val){

        var data= await TILT_Tag_NoteSaveMultiple(val);
        if(data == null)
            return null;

        return data[0];
                
    }
    public async  Task<TILT_Tag_Note[]> TILT_Tag_NoteSaveMultiple(params TILT_Tag_Note[] values){

        if(values == null)
            return null;
        if(values.Length == 0)
            return values;
        foreach(var value in values){
            this.TILT_Tag_Note.Add(value);
        }
        await SaveChangesAsync();
        return values;

                
    }

    public  IAsyncEnumerable<TILT_Tag_Note> TILT_Tag_NoteSimpleSearch(GeneratorFromDB.SearchCriteria sc, eTILT_Tag_NoteColumns colToSearch, string value){
        
 var search = SearchTILT_Tag_Note.FromSearch(sc,colToSearch,value);
 /*
        var orderBy = new GeneratorFromDB.OrderBy<eTILT_Tag_NoteColumns>();
                    orderBy.FieldName = colToSearch;
                orderBy.Asc = false;
        search.OrderBys = new[] { orderBy };
        search.PageNumber = 1;
        search.PageSize = int.MaxValue-1;
        var s = new GeneratorFromDB.SearchField<eTILT_Tag_NoteColumns>();
        s.Criteria = sc;
        s.FieldName = colToSearch;
        s.Value = value;
        search.SearchFields = new[] { s };
        */
        var data = this.TILT_Tag_NoteFind_AsyncEnumerable(search);
        return data;
    }   
/*
    public IAsyncEnumerable<TILT_Tag_Note> TILT_Tag_NoteFind_AsyncEnumerable(SearchTILT_Tag_Note? search){
 
        IQueryable<TILT_Tag_Note> data= this.TILT_Tag_Note ;
        if(search == null){
            return data.AsAsyncEnumerable();
        }
        data = search.TransformToWhere(data);        
        IQueryable<TILT_Tag_Note> =search.TransformToOrder(data);
        if(search.PageSize>1)
            ret= ret.Skip((search.PageNumber-1)*search.PageSize);
            
        ret=ret.Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    }
*/
    public Task<long> TILT_Tag_NoteCount( SearchTILT_Tag_Note? search){
     
        IQueryable<TILT_Tag_Note> data= this.TILT_Tag_Note ;
        if(search != null){
            data = search.TransformToWhere(data);        
        }        
        return data.LongCountAsync();
    }
    

    

    public IAsyncEnumerable<TILT_Tag_Note> TILT_Tag_NoteFind_AsyncEnumerable(SearchTILT_Tag_Note? search){
        IQueryable<TILT_Tag_Note> data= this.TILT_Tag_Note ;
        if(search == null){
                        return data.AsAsyncEnumerable();
                    }
        data = search.TransformToWhere(data); 
        
        IQueryable<TILT_Tag_Note> ret= search.TransformToOrder(data);
        if(search.PageNumber>1)
            ret=ret.Skip((search.PageNumber-1)*search.PageSize);
            
        ret=ret.Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    
    }

        public async Task<TILT_URL[]> TILT_URLFind_Array( SearchTILT_URL? search){
 
        IQueryable<TILT_URL> data= this.TILT_URL ;
        if(search == null){
            return await data.ToArrayAsync();
        }
        data = search.TransformToWhere(data);        
        IQueryable<TILT_URL> ret= search.TransformToOrder(data);
        if(search.PageNumber>1)
            ret= ret.Skip((search.PageNumber-1)*search.PageSize);
            
        ret=ret.Take(search.PageSize);
        return await ret.ToArrayAsync();
    }

    public  IAsyncEnumerable<TILT_URL> TILT_URLGetAll(){
        return this.TILT_URLFind_AsyncEnumerable(null);
    }
        public async  Task<TILT_URL> TILT_URLSave(TILT_URL val){

        var data= await TILT_URLSaveMultiple(val);
        if(data == null)
            return null;

        return data[0];
                
    }
    public async  Task<TILT_URL[]> TILT_URLSaveMultiple(params TILT_URL[] values){

        if(values == null)
            return null;
        if(values.Length == 0)
            return values;
        foreach(var value in values){
            this.TILT_URL.Add(value);
        }
        await SaveChangesAsync();
        return values;

                
    }

    public  IAsyncEnumerable<TILT_URL> TILT_URLSimpleSearch(GeneratorFromDB.SearchCriteria sc, eTILT_URLColumns colToSearch, string value){
        
 var search = SearchTILT_URL.FromSearch(sc,colToSearch,value);
 /*
        var orderBy = new GeneratorFromDB.OrderBy<eTILT_URLColumns>();
                    orderBy.FieldName = colToSearch;
                orderBy.Asc = false;
        search.OrderBys = new[] { orderBy };
        search.PageNumber = 1;
        search.PageSize = int.MaxValue-1;
        var s = new GeneratorFromDB.SearchField<eTILT_URLColumns>();
        s.Criteria = sc;
        s.FieldName = colToSearch;
        s.Value = value;
        search.SearchFields = new[] { s };
        */
        var data = this.TILT_URLFind_AsyncEnumerable(search);
        return data;
    }   
/*
    public IAsyncEnumerable<TILT_URL> TILT_URLFind_AsyncEnumerable(SearchTILT_URL? search){
 
        IQueryable<TILT_URL> data= this.TILT_URL ;
        if(search == null){
            return data.AsAsyncEnumerable();
        }
        data = search.TransformToWhere(data);        
        IQueryable<TILT_URL> =search.TransformToOrder(data);
        if(search.PageSize>1)
            ret= ret.Skip((search.PageNumber-1)*search.PageSize);
            
        ret=ret.Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    }
*/
    public Task<long> TILT_URLCount( SearchTILT_URL? search){
     
        IQueryable<TILT_URL> data= this.TILT_URL ;
        if(search != null){
            data = search.TransformToWhere(data);        
        }        
        return data.LongCountAsync();
    }
    

    

    public IAsyncEnumerable<TILT_URL> TILT_URLFind_AsyncEnumerable(SearchTILT_URL? search){
        IQueryable<TILT_URL> data= this.TILT_URL ;
        if(search == null){
                        return data.AsAsyncEnumerable();
                    }
        data = search.TransformToWhere(data); 
        
        IQueryable<TILT_URL> ret= search.TransformToOrder(data);
        if(search.PageNumber>1)
            ret=ret.Skip((search.PageNumber-1)*search.PageSize);
            
        ret=ret.Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    
    }

}
public interface I_InsertDataApplicationDBContext{
        Task<TILT_Note_Table?> InsertTILT_Note(TILT_Note_Table value);
        Task<TILT_Note_Table[]> InsertTILT_Notes(params TILT_Note_Table[] values);

        Task<TILT_Tag_Table?> InsertTILT_Tag(TILT_Tag_Table value);
        Task<TILT_Tag_Table[]> InsertTILT_Tags(params TILT_Tag_Table[] values);

        Task<TILT_Tag_Note_Table?> InsertTILT_Tag_Note(TILT_Tag_Note_Table value);
        Task<TILT_Tag_Note_Table[]> InsertTILT_Tag_Notes(params TILT_Tag_Note_Table[] values);

        Task<TILT_URL_Table?> InsertTILT_URL(TILT_URL_Table value);
        Task<TILT_URL_Table[]> InsertTILT_URLs(params TILT_URL_Table[] values);

    }

public class InsertDataApplicationDBContext: I_InsertDataApplicationDBContext{

        private ApplicationDBContext _context;
        public InsertDataApplicationDBContext(ApplicationDBContext context){
            _context=context;
        }
        public async Task<TILT_Note_Table?> InsertTILT_Note(TILT_Note_Table value){
            if (value == null)
                return null;

            TILT_Note val = (TILT_Note)value!;
            _context.TILT_Note.Add(val);
            await _context.SaveChangesAsync();
            return (TILT_Note_Table)val! ;

        }
        public async Task<TILT_Note_Table[]> InsertTILT_Notes(params TILT_Note_Table[] values){
        
        if (values == null || values.Length == 0)
            return new TILT_Note_Table[0];

        TILT_Note[] vals = values.Select(it=>(TILT_Note)it!).ToArray();
        _context.TILT_Note.AddRange(vals);
        await _context.SaveChangesAsync();
        return vals.Select(it => (TILT_Note_Table)it!  ).ToArray();
    }
    
        public async Task<TILT_Tag_Table?> InsertTILT_Tag(TILT_Tag_Table value){
            if (value == null)
                return null;

            TILT_Tag val = (TILT_Tag)value!;
            _context.TILT_Tag.Add(val);
            await _context.SaveChangesAsync();
            return (TILT_Tag_Table)val! ;

        }
        public async Task<TILT_Tag_Table[]> InsertTILT_Tags(params TILT_Tag_Table[] values){
        
        if (values == null || values.Length == 0)
            return new TILT_Tag_Table[0];

        TILT_Tag[] vals = values.Select(it=>(TILT_Tag)it!).ToArray();
        _context.TILT_Tag.AddRange(vals);
        await _context.SaveChangesAsync();
        return vals.Select(it => (TILT_Tag_Table)it!  ).ToArray();
    }
    
        public async Task<TILT_Tag_Note_Table?> InsertTILT_Tag_Note(TILT_Tag_Note_Table value){
            if (value == null)
                return null;

            TILT_Tag_Note val = (TILT_Tag_Note)value!;
            _context.TILT_Tag_Note.Add(val);
            await _context.SaveChangesAsync();
            return (TILT_Tag_Note_Table)val! ;

        }
        public async Task<TILT_Tag_Note_Table[]> InsertTILT_Tag_Notes(params TILT_Tag_Note_Table[] values){
        
        if (values == null || values.Length == 0)
            return new TILT_Tag_Note_Table[0];

        TILT_Tag_Note[] vals = values.Select(it=>(TILT_Tag_Note)it!).ToArray();
        _context.TILT_Tag_Note.AddRange(vals);
        await _context.SaveChangesAsync();
        return vals.Select(it => (TILT_Tag_Note_Table)it!  ).ToArray();
    }
    
        public async Task<TILT_URL_Table?> InsertTILT_URL(TILT_URL_Table value){
            if (value == null)
                return null;

            TILT_URL val = (TILT_URL)value!;
            _context.TILT_URL.Add(val);
            await _context.SaveChangesAsync();
            return (TILT_URL_Table)val! ;

        }
        public async Task<TILT_URL_Table[]> InsertTILT_URLs(params TILT_URL_Table[] values){
        
        if (values == null || values.Length == 0)
            return new TILT_URL_Table[0];

        TILT_URL[] vals = values.Select(it=>(TILT_URL)it!).ToArray();
        _context.TILT_URL.AddRange(vals);
        await _context.SaveChangesAsync();
        return vals.Select(it => (TILT_URL_Table)it!  ).ToArray();
    }
    
    
}//end class InsertDataApplicationDBContext

   public interface ISearchDataTILT_Note {
        IAsyncEnumerable<TILT_Note> TILT_NoteFind_AsyncEnumerable(SearchTILT_Note? search);
    
    public Task<long> GetAllCount();
    public Task<long> GetAllCount(SearchTILT_Note? search);

        
    
    public  IAsyncEnumerable<TILT_Note> TILT_NoteSimpleSearch_ID(GeneratorFromDB.SearchCriteria sc,  long value);
    public  IAsyncEnumerable<TILT_Note> TILT_NoteSimpleSearchNull_ID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<TILT_Note> TILT_NoteSimpleSearch_IDURL(GeneratorFromDB.SearchCriteria sc,  long value);
    public  IAsyncEnumerable<TILT_Note> TILT_NoteSimpleSearchNull_IDURL(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<TILT_Note> TILT_NoteSimpleSearch_Text(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<TILT_Note> TILT_NoteSimpleSearchNull_Text(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<TILT_Note> TILT_NoteSimpleSearch_Link(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<TILT_Note> TILT_NoteSimpleSearchNull_Link(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<TILT_Note> TILT_NoteSimpleSearch_ForDate(GeneratorFromDB.SearchCriteria sc,  DateTime? value);
    public  IAsyncEnumerable<TILT_Note> TILT_NoteSimpleSearchNull_ForDate(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<TILT_Note> TILT_NoteSimpleSearch_TimeZoneString(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<TILT_Note> TILT_NoteSimpleSearchNull_TimeZoneString(GeneratorFromDB.SearchCriteria sc);
        } //interface searchdata   

   public class SearchDataTILT_Note: ISearchDataTILT_Note{
        private ApplicationDBContext context;
        public SearchDataTILT_Note (ApplicationDBContext context) {
            this.context=context;
        }
           
        public async Task<long> GetAllCount(){
            return await context.TILT_NoteCount(null);
        }
        public async Task<long> GetAllCount(SearchTILT_Note? search){
            return await context.TILT_NoteCount(search);
        }
        public IAsyncEnumerable<TILT_Note> TILT_NoteFind_AsyncEnumerable(SearchTILT_Note? search){
            return context.TILT_NoteFind_AsyncEnumerable(search);
        }
        public  IAsyncEnumerable<TILT_Note> TILT_NoteSimpleSearch(GeneratorFromDB.SearchCriteria sc, eTILT_NoteColumns colToSearch, string? value){
        var search =SearchTILT_Note.FromSearch(sc,colToSearch,value);
    /*
            var search = new SearchTILT_Note();
            var orderBy = new GeneratorFromDB.OrderBy<eTILT_NoteColumns>();
                              orderBy.FieldName = colToSearch;
          
            orderBy.Asc = false;
            search.OrderBys = new[] { orderBy };
            search.PageNumber = 1;
            search.PageSize = int.MaxValue;
            var s = new GeneratorFromDB.SearchField<eTILT_NoteColumns>();
            s.Criteria = sc;
            s.FieldName = colToSearch;
            s.Value = value;
            search.SearchFields = new[] { s };
       */
            var data = this.TILT_NoteFind_AsyncEnumerable(search);
            return data;
        }

    
        //False
    public  IAsyncEnumerable<TILT_Note> TILT_NoteSimpleSearch_ID(GeneratorFromDB.SearchCriteria sc,  long value){
         return TILT_NoteSimpleSearch(sc,eTILT_NoteColumns.ID,value.ToString());

    
    }
    public  IAsyncEnumerable<TILT_Note> TILT_NoteSimpleSearchNull_ID(GeneratorFromDB.SearchCriteria sc){
        return TILT_NoteSimpleSearch(sc,eTILT_NoteColumns.ID,null);

    }


        //False
    public  IAsyncEnumerable<TILT_Note> TILT_NoteSimpleSearch_IDURL(GeneratorFromDB.SearchCriteria sc,  long value){
         return TILT_NoteSimpleSearch(sc,eTILT_NoteColumns.IDURL,value.ToString());

    
    }
    public  IAsyncEnumerable<TILT_Note> TILT_NoteSimpleSearchNull_IDURL(GeneratorFromDB.SearchCriteria sc){
        return TILT_NoteSimpleSearch(sc,eTILT_NoteColumns.IDURL,null);

    }


        //False
    public  IAsyncEnumerable<TILT_Note> TILT_NoteSimpleSearch_Text(GeneratorFromDB.SearchCriteria sc,  string value){
         return TILT_NoteSimpleSearch(sc,eTILT_NoteColumns.Text,value.ToString());

    
    }
    public  IAsyncEnumerable<TILT_Note> TILT_NoteSimpleSearchNull_Text(GeneratorFromDB.SearchCriteria sc){
        return TILT_NoteSimpleSearch(sc,eTILT_NoteColumns.Text,null);

    }


        //True
    public  IAsyncEnumerable<TILT_Note> TILT_NoteSimpleSearch_Link(GeneratorFromDB.SearchCriteria sc,  string value){
         return TILT_NoteSimpleSearch(sc,eTILT_NoteColumns.Link,value?.ToString());

    
    }
    public  IAsyncEnumerable<TILT_Note> TILT_NoteSimpleSearchNull_Link(GeneratorFromDB.SearchCriteria sc){
        return TILT_NoteSimpleSearch(sc,eTILT_NoteColumns.Link,null);

    }


        //True
    public  IAsyncEnumerable<TILT_Note> TILT_NoteSimpleSearch_ForDate(GeneratorFromDB.SearchCriteria sc,  DateTime? value){
         return TILT_NoteSimpleSearch(sc,eTILT_NoteColumns.ForDate,value?.ToString());

    
    }
    public  IAsyncEnumerable<TILT_Note> TILT_NoteSimpleSearchNull_ForDate(GeneratorFromDB.SearchCriteria sc){
        return TILT_NoteSimpleSearch(sc,eTILT_NoteColumns.ForDate,null);

    }


        //True
    public  IAsyncEnumerable<TILT_Note> TILT_NoteSimpleSearch_TimeZoneString(GeneratorFromDB.SearchCriteria sc,  string value){
         return TILT_NoteSimpleSearch(sc,eTILT_NoteColumns.TimeZoneString,value?.ToString());

    
    }
    public  IAsyncEnumerable<TILT_Note> TILT_NoteSimpleSearchNull_TimeZoneString(GeneratorFromDB.SearchCriteria sc){
        return TILT_NoteSimpleSearch(sc,eTILT_NoteColumns.TimeZoneString,null);

    }


        } //class searchdata




    
   public interface ISearchDataTILT_Tag {
        IAsyncEnumerable<TILT_Tag> TILT_TagFind_AsyncEnumerable(SearchTILT_Tag? search);
    
    public Task<long> GetAllCount();
    public Task<long> GetAllCount(SearchTILT_Tag? search);

        
    
    public  IAsyncEnumerable<TILT_Tag> TILT_TagSimpleSearch_ID(GeneratorFromDB.SearchCriteria sc,  long value);
    public  IAsyncEnumerable<TILT_Tag> TILT_TagSimpleSearchNull_ID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<TILT_Tag> TILT_TagSimpleSearch_Name(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<TILT_Tag> TILT_TagSimpleSearchNull_Name(GeneratorFromDB.SearchCriteria sc);
        } //interface searchdata   

   public class SearchDataTILT_Tag: ISearchDataTILT_Tag{
        private ApplicationDBContext context;
        public SearchDataTILT_Tag (ApplicationDBContext context) {
            this.context=context;
        }
           
        public async Task<long> GetAllCount(){
            return await context.TILT_TagCount(null);
        }
        public async Task<long> GetAllCount(SearchTILT_Tag? search){
            return await context.TILT_TagCount(search);
        }
        public IAsyncEnumerable<TILT_Tag> TILT_TagFind_AsyncEnumerable(SearchTILT_Tag? search){
            return context.TILT_TagFind_AsyncEnumerable(search);
        }
        public  IAsyncEnumerable<TILT_Tag> TILT_TagSimpleSearch(GeneratorFromDB.SearchCriteria sc, eTILT_TagColumns colToSearch, string? value){
        var search =SearchTILT_Tag.FromSearch(sc,colToSearch,value);
    /*
            var search = new SearchTILT_Tag();
            var orderBy = new GeneratorFromDB.OrderBy<eTILT_TagColumns>();
                              orderBy.FieldName = colToSearch;
          
            orderBy.Asc = false;
            search.OrderBys = new[] { orderBy };
            search.PageNumber = 1;
            search.PageSize = int.MaxValue;
            var s = new GeneratorFromDB.SearchField<eTILT_TagColumns>();
            s.Criteria = sc;
            s.FieldName = colToSearch;
            s.Value = value;
            search.SearchFields = new[] { s };
       */
            var data = this.TILT_TagFind_AsyncEnumerable(search);
            return data;
        }

    
        //False
    public  IAsyncEnumerable<TILT_Tag> TILT_TagSimpleSearch_ID(GeneratorFromDB.SearchCriteria sc,  long value){
         return TILT_TagSimpleSearch(sc,eTILT_TagColumns.ID,value.ToString());

    
    }
    public  IAsyncEnumerable<TILT_Tag> TILT_TagSimpleSearchNull_ID(GeneratorFromDB.SearchCriteria sc){
        return TILT_TagSimpleSearch(sc,eTILT_TagColumns.ID,null);

    }


        //False
    public  IAsyncEnumerable<TILT_Tag> TILT_TagSimpleSearch_Name(GeneratorFromDB.SearchCriteria sc,  string value){
         return TILT_TagSimpleSearch(sc,eTILT_TagColumns.Name,value.ToString());

    
    }
    public  IAsyncEnumerable<TILT_Tag> TILT_TagSimpleSearchNull_Name(GeneratorFromDB.SearchCriteria sc){
        return TILT_TagSimpleSearch(sc,eTILT_TagColumns.Name,null);

    }


        } //class searchdata




    
   public interface ISearchDataTILT_Tag_Note {
        IAsyncEnumerable<TILT_Tag_Note> TILT_Tag_NoteFind_AsyncEnumerable(SearchTILT_Tag_Note? search);
    
    public Task<long> GetAllCount();
    public Task<long> GetAllCount(SearchTILT_Tag_Note? search);

        
    
    public  IAsyncEnumerable<TILT_Tag_Note> TILT_Tag_NoteSimpleSearch_ID(GeneratorFromDB.SearchCriteria sc,  long value);
    public  IAsyncEnumerable<TILT_Tag_Note> TILT_Tag_NoteSimpleSearchNull_ID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<TILT_Tag_Note> TILT_Tag_NoteSimpleSearch_IDTag(GeneratorFromDB.SearchCriteria sc,  long value);
    public  IAsyncEnumerable<TILT_Tag_Note> TILT_Tag_NoteSimpleSearchNull_IDTag(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<TILT_Tag_Note> TILT_Tag_NoteSimpleSearch_IDNote(GeneratorFromDB.SearchCriteria sc,  long value);
    public  IAsyncEnumerable<TILT_Tag_Note> TILT_Tag_NoteSimpleSearchNull_IDNote(GeneratorFromDB.SearchCriteria sc);
        } //interface searchdata   

   public class SearchDataTILT_Tag_Note: ISearchDataTILT_Tag_Note{
        private ApplicationDBContext context;
        public SearchDataTILT_Tag_Note (ApplicationDBContext context) {
            this.context=context;
        }
           
        public async Task<long> GetAllCount(){
            return await context.TILT_Tag_NoteCount(null);
        }
        public async Task<long> GetAllCount(SearchTILT_Tag_Note? search){
            return await context.TILT_Tag_NoteCount(search);
        }
        public IAsyncEnumerable<TILT_Tag_Note> TILT_Tag_NoteFind_AsyncEnumerable(SearchTILT_Tag_Note? search){
            return context.TILT_Tag_NoteFind_AsyncEnumerable(search);
        }
        public  IAsyncEnumerable<TILT_Tag_Note> TILT_Tag_NoteSimpleSearch(GeneratorFromDB.SearchCriteria sc, eTILT_Tag_NoteColumns colToSearch, string? value){
        var search =SearchTILT_Tag_Note.FromSearch(sc,colToSearch,value);
    /*
            var search = new SearchTILT_Tag_Note();
            var orderBy = new GeneratorFromDB.OrderBy<eTILT_Tag_NoteColumns>();
                              orderBy.FieldName = colToSearch;
          
            orderBy.Asc = false;
            search.OrderBys = new[] { orderBy };
            search.PageNumber = 1;
            search.PageSize = int.MaxValue;
            var s = new GeneratorFromDB.SearchField<eTILT_Tag_NoteColumns>();
            s.Criteria = sc;
            s.FieldName = colToSearch;
            s.Value = value;
            search.SearchFields = new[] { s };
       */
            var data = this.TILT_Tag_NoteFind_AsyncEnumerable(search);
            return data;
        }

    
        //False
    public  IAsyncEnumerable<TILT_Tag_Note> TILT_Tag_NoteSimpleSearch_ID(GeneratorFromDB.SearchCriteria sc,  long value){
         return TILT_Tag_NoteSimpleSearch(sc,eTILT_Tag_NoteColumns.ID,value.ToString());

    
    }
    public  IAsyncEnumerable<TILT_Tag_Note> TILT_Tag_NoteSimpleSearchNull_ID(GeneratorFromDB.SearchCriteria sc){
        return TILT_Tag_NoteSimpleSearch(sc,eTILT_Tag_NoteColumns.ID,null);

    }


        //False
    public  IAsyncEnumerable<TILT_Tag_Note> TILT_Tag_NoteSimpleSearch_IDTag(GeneratorFromDB.SearchCriteria sc,  long value){
         return TILT_Tag_NoteSimpleSearch(sc,eTILT_Tag_NoteColumns.IDTag,value.ToString());

    
    }
    public  IAsyncEnumerable<TILT_Tag_Note> TILT_Tag_NoteSimpleSearchNull_IDTag(GeneratorFromDB.SearchCriteria sc){
        return TILT_Tag_NoteSimpleSearch(sc,eTILT_Tag_NoteColumns.IDTag,null);

    }


        //False
    public  IAsyncEnumerable<TILT_Tag_Note> TILT_Tag_NoteSimpleSearch_IDNote(GeneratorFromDB.SearchCriteria sc,  long value){
         return TILT_Tag_NoteSimpleSearch(sc,eTILT_Tag_NoteColumns.IDNote,value.ToString());

    
    }
    public  IAsyncEnumerable<TILT_Tag_Note> TILT_Tag_NoteSimpleSearchNull_IDNote(GeneratorFromDB.SearchCriteria sc){
        return TILT_Tag_NoteSimpleSearch(sc,eTILT_Tag_NoteColumns.IDNote,null);

    }


        } //class searchdata




    
   public interface ISearchDataTILT_URL {
        IAsyncEnumerable<TILT_URL> TILT_URLFind_AsyncEnumerable(SearchTILT_URL? search);
    
    public Task<long> GetAllCount();
    public Task<long> GetAllCount(SearchTILT_URL? search);

        
    
    public  IAsyncEnumerable<TILT_URL> TILT_URLSimpleSearch_ID(GeneratorFromDB.SearchCriteria sc,  long value);
    public  IAsyncEnumerable<TILT_URL> TILT_URLSimpleSearchNull_ID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<TILT_URL> TILT_URLSimpleSearch_URLPart(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<TILT_URL> TILT_URLSimpleSearchNull_URLPart(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<TILT_URL> TILT_URLSimpleSearch_Secret(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<TILT_URL> TILT_URLSimpleSearchNull_Secret(GeneratorFromDB.SearchCriteria sc);
        } //interface searchdata   

   public class SearchDataTILT_URL: ISearchDataTILT_URL{
        private ApplicationDBContext context;
        public SearchDataTILT_URL (ApplicationDBContext context) {
            this.context=context;
        }
           
        public async Task<long> GetAllCount(){
            return await context.TILT_URLCount(null);
        }
        public async Task<long> GetAllCount(SearchTILT_URL? search){
            return await context.TILT_URLCount(search);
        }
        public IAsyncEnumerable<TILT_URL> TILT_URLFind_AsyncEnumerable(SearchTILT_URL? search){
            return context.TILT_URLFind_AsyncEnumerable(search);
        }
        public  IAsyncEnumerable<TILT_URL> TILT_URLSimpleSearch(GeneratorFromDB.SearchCriteria sc, eTILT_URLColumns colToSearch, string? value){
        var search =SearchTILT_URL.FromSearch(sc,colToSearch,value);
    /*
            var search = new SearchTILT_URL();
            var orderBy = new GeneratorFromDB.OrderBy<eTILT_URLColumns>();
                              orderBy.FieldName = colToSearch;
          
            orderBy.Asc = false;
            search.OrderBys = new[] { orderBy };
            search.PageNumber = 1;
            search.PageSize = int.MaxValue;
            var s = new GeneratorFromDB.SearchField<eTILT_URLColumns>();
            s.Criteria = sc;
            s.FieldName = colToSearch;
            s.Value = value;
            search.SearchFields = new[] { s };
       */
            var data = this.TILT_URLFind_AsyncEnumerable(search);
            return data;
        }

    
        //False
    public  IAsyncEnumerable<TILT_URL> TILT_URLSimpleSearch_ID(GeneratorFromDB.SearchCriteria sc,  long value){
         return TILT_URLSimpleSearch(sc,eTILT_URLColumns.ID,value.ToString());

    
    }
    public  IAsyncEnumerable<TILT_URL> TILT_URLSimpleSearchNull_ID(GeneratorFromDB.SearchCriteria sc){
        return TILT_URLSimpleSearch(sc,eTILT_URLColumns.ID,null);

    }


        //False
    public  IAsyncEnumerable<TILT_URL> TILT_URLSimpleSearch_URLPart(GeneratorFromDB.SearchCriteria sc,  string value){
         return TILT_URLSimpleSearch(sc,eTILT_URLColumns.URLPart,value.ToString());

    
    }
    public  IAsyncEnumerable<TILT_URL> TILT_URLSimpleSearchNull_URLPart(GeneratorFromDB.SearchCriteria sc){
        return TILT_URLSimpleSearch(sc,eTILT_URLColumns.URLPart,null);

    }


        //False
    public  IAsyncEnumerable<TILT_URL> TILT_URLSimpleSearch_Secret(GeneratorFromDB.SearchCriteria sc,  string value){
         return TILT_URLSimpleSearch(sc,eTILT_URLColumns.Secret,value.ToString());

    
    }
    public  IAsyncEnumerable<TILT_URL> TILT_URLSimpleSearchNull_Secret(GeneratorFromDB.SearchCriteria sc){
        return TILT_URLSimpleSearch(sc,eTILT_URLColumns.Secret,null);

    }


        } //class searchdata




    
   


//end added new




