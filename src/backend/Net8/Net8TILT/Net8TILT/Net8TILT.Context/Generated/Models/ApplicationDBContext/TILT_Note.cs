﻿//2.this was autogenerated by a tool. Do not modify! Use partial
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

//modified 2023.12.15
namespace Generated;

public partial class SearchTILT_Note:  GeneratorFromDB.Search<eTILT_NoteColumns,TILT_Note>
{
    //private ApplicationDBContext _context;
    //public SearchTILT_Note(ApplicationDBContext context){
    //    _context= context;
    //}
public static SearchTILT_Note FromSearch(GeneratorFromDB.SearchCriteria sc, eTILT_NoteColumns colToSearch, string value)
    {
        var search = new SearchTILT_Note();
        var orderBy = new GeneratorFromDB.OrderBy<eTILT_NoteColumns>();
                orderBy.FieldName = eTILT_NoteColumns.ID ;;
        
        orderBy.Asc = false;
        search.OrderBys = new[] { orderBy };
        search.PageNumber = 1;
        search.PageSize = int.MaxValue - 1;
        var s = new GeneratorFromDB.SearchField<eTILT_NoteColumns>();
        s.Criteria = sc;
        s.FieldName = colToSearch;
        s.Value = value;
        search.SearchFields = new[] { s };
        return search;
    }
   public override IOrderedQueryable<TILT_Note> TransformToOrder(IQueryable<TILT_Note> data){
        if(OrderBys == null || OrderBys.Length ==0){
            OrderBys =new GeneratorFromDB.OrderBy<eTILT_NoteColumns>[1];
            OrderBys[0]= new GeneratorFromDB.OrderBy<eTILT_NoteColumns>()
            {
                //maybe find PK ...
                FieldName = eTILT_NoteColumns.ID,
                Asc=false
            };
        }
        var order = OrderBys[0]!;
        IOrderedQueryable<TILT_Note> ret;
        //TODO: maybe utilize EF.Property ? 
        switch(order.FieldName){
                    case eTILT_NoteColumns.ID:
                if(order.Asc)
                    ret = data.OrderBy(it=>it.ID);
                else
                    ret = data.OrderByDescending(it=>it.ID);
                
                break;

                    case eTILT_NoteColumns.IDURL:
                if(order.Asc)
                    ret = data.OrderBy(it=>it.IDURL);
                else
                    ret = data.OrderByDescending(it=>it.IDURL);
                
                break;

                    case eTILT_NoteColumns.Text:
                if(order.Asc)
                    ret = data.OrderBy(it=>it.Text);
                else
                    ret = data.OrderByDescending(it=>it.Text);
                
                break;

                    case eTILT_NoteColumns.Link:
                if(order.Asc)
                    ret = data.OrderBy(it=>it.Link);
                else
                    ret = data.OrderByDescending(it=>it.Link);
                
                break;

                    case eTILT_NoteColumns.ForDate:
                if(order.Asc)
                    ret = data.OrderBy(it=>it.ForDate);
                else
                    ret = data.OrderByDescending(it=>it.ForDate);
                
                break;

                    case eTILT_NoteColumns.TimeZoneString:
                if(order.Asc)
                    ret = data.OrderBy(it=>it.TimeZoneString);
                else
                    ret = data.OrderByDescending(it=>it.TimeZoneString);
                
                break;

                    default:
                throw new ArgumentException(" cannot order TILT_Note by "+ order.FieldName);
            
        }
        for(var i=1;i<OrderBys.Length;i++){
            order=OrderBys[i];
            switch(order.FieldName){
                    case eTILT_NoteColumns.ID:
                if(order.Asc)
                    ret = ret.ThenBy(it=>it.ID);
                else
                    ret = ret.ThenByDescending(it=>it.ID);
                
                break;
                    case eTILT_NoteColumns.IDURL:
                if(order.Asc)
                    ret = ret.ThenBy(it=>it.IDURL);
                else
                    ret = ret.ThenByDescending(it=>it.IDURL);
                
                break;
                    case eTILT_NoteColumns.Text:
                if(order.Asc)
                    ret = ret.ThenBy(it=>it.Text);
                else
                    ret = ret.ThenByDescending(it=>it.Text);
                
                break;
                    case eTILT_NoteColumns.Link:
                if(order.Asc)
                    ret = ret.ThenBy(it=>it.Link);
                else
                    ret = ret.ThenByDescending(it=>it.Link);
                
                break;
                    case eTILT_NoteColumns.ForDate:
                if(order.Asc)
                    ret = ret.ThenBy(it=>it.ForDate);
                else
                    ret = ret.ThenByDescending(it=>it.ForDate);
                
                break;
                    case eTILT_NoteColumns.TimeZoneString:
                if(order.Asc)
                    ret = ret.ThenBy(it=>it.TimeZoneString);
                else
                    ret = ret.ThenByDescending(it=>it.TimeZoneString);
                
                break;
                    default:
                throw new ArgumentException(" cannot order TILT_Note by "+ order.FieldName);
            
        }
        }
        return ret;
        
    }
    public override  IQueryable<TILT_Note> TransformToWhere(IQueryable<TILT_Note> data){
        if(SearchFields == null || SearchFields.Length ==0)        
            return data;
        var returnValue = data;
        foreach(var s in SearchFields){
            switch(s.FieldName ){
                case eTILT_NoteColumns.None :
                    continue;
                    
            case eTILT_NoteColumns.ID:
                //long isNullable False
                if(s.Value == null)
        {
                            throw new ArgumentException("TILT_Note.ID cannot be null");
                                }//end if s.value is null -search for null
        //if we are here, s.Value is not null
        { //use this to define value in smaller scope
                        var valueArray = s.Value
                    .Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(it=>long.Parse(it))
                    .ToArray();
                var value = valueArray[0];
                  
          
        switch(s.Criteria){

            case GeneratorFromDB.SearchCriteria.Between:
                if(valueArray?.Length != 2){
                    throw new ArgumentException("between must have 2 args, separated by comma => value is:" + s.Value);
                }
                                    returnValue =returnValue.Where(it=>it.ID >= valueArray[0] && it.ID <= valueArray[1]);
                  
                continue;
            case GeneratorFromDB.SearchCriteria.NotBetween:
            
            if(valueArray?.Length != 2){
                    throw new ArgumentException("not between must have 2 args, separated by comma => value is:" + s.Value);
                }
                                    returnValue =returnValue.Where(it=>it.ID < valueArray[0] || it.ID > valueArray[1]);
                  
                continue;
            case GeneratorFromDB.SearchCriteria.InArray:
                        returnValue =returnValue.Where(it=> valueArray!.Contains(it.ID));
                  
                continue;
            case GeneratorFromDB.SearchCriteria.NotInArray:
                        returnValue =returnValue.Where(it=> !valueArray!.Contains(it.ID));
                  
                continue;
            case GeneratorFromDB.SearchCriteria.Equal:
                returnValue =returnValue.Where(it=>it.ID==value);
                continue;
            case GeneratorFromDB.SearchCriteria.Different:
                returnValue =returnValue.Where(it=>it.ID!=value);
                continue;
            
            case GeneratorFromDB.SearchCriteria.Less:
                                                returnValue =returnValue.Where(it=>it.ID<value);
                                                
                        continue;
                    case GeneratorFromDB.SearchCriteria.LessOrEqual:
                                                
                        returnValue =returnValue.Where(it=>it.ID<=value);
                                                
                        continue;
                    case GeneratorFromDB.SearchCriteria.Greater:
                                                
                        returnValue =returnValue.Where(it=>it.ID>value);
                                                continue;
                    case GeneratorFromDB.SearchCriteria.GreaterOrEqual:
                                                returnValue =returnValue.Where(it=>it.ID>=value);
                                                continue;
                                                                        

            default:
                throw new ArgumentException($"not found Criteria {(int)s.Criteria} {s.Criteria} for {s.FieldName}");
        }//end switch after s.Criteria

                //continue;
        } //end use this to define value in smaller scope
            
                    
            case eTILT_NoteColumns.IDURL:
                //long isNullable False
                if(s.Value == null)
        {
                            throw new ArgumentException("TILT_Note.IDURL cannot be null");
                                }//end if s.value is null -search for null
        //if we are here, s.Value is not null
        { //use this to define value in smaller scope
                        var valueArray = s.Value
                    .Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(it=>long.Parse(it))
                    .ToArray();
                var value = valueArray[0];
                  
          
        switch(s.Criteria){

            case GeneratorFromDB.SearchCriteria.Between:
                if(valueArray?.Length != 2){
                    throw new ArgumentException("between must have 2 args, separated by comma => value is:" + s.Value);
                }
                                    returnValue =returnValue.Where(it=>it.IDURL >= valueArray[0] && it.IDURL <= valueArray[1]);
                  
                continue;
            case GeneratorFromDB.SearchCriteria.NotBetween:
            
            if(valueArray?.Length != 2){
                    throw new ArgumentException("not between must have 2 args, separated by comma => value is:" + s.Value);
                }
                                    returnValue =returnValue.Where(it=>it.IDURL < valueArray[0] || it.IDURL > valueArray[1]);
                  
                continue;
            case GeneratorFromDB.SearchCriteria.InArray:
                        returnValue =returnValue.Where(it=> valueArray!.Contains(it.IDURL));
                  
                continue;
            case GeneratorFromDB.SearchCriteria.NotInArray:
                        returnValue =returnValue.Where(it=> !valueArray!.Contains(it.IDURL));
                  
                continue;
            case GeneratorFromDB.SearchCriteria.Equal:
                returnValue =returnValue.Where(it=>it.IDURL==value);
                continue;
            case GeneratorFromDB.SearchCriteria.Different:
                returnValue =returnValue.Where(it=>it.IDURL!=value);
                continue;
            
            case GeneratorFromDB.SearchCriteria.Less:
                                                returnValue =returnValue.Where(it=>it.IDURL<value);
                                                
                        continue;
                    case GeneratorFromDB.SearchCriteria.LessOrEqual:
                                                
                        returnValue =returnValue.Where(it=>it.IDURL<=value);
                                                
                        continue;
                    case GeneratorFromDB.SearchCriteria.Greater:
                                                
                        returnValue =returnValue.Where(it=>it.IDURL>value);
                                                continue;
                    case GeneratorFromDB.SearchCriteria.GreaterOrEqual:
                                                returnValue =returnValue.Where(it=>it.IDURL>=value);
                                                continue;
                                                                        

            default:
                throw new ArgumentException($"not found Criteria {(int)s.Criteria} {s.Criteria} for {s.FieldName}");
        }//end switch after s.Criteria

                //continue;
        } //end use this to define value in smaller scope
            
                    
            case eTILT_NoteColumns.Text:
                //string isNullable False
                if(s.Value == null)
        {
                            switch(s.Criteria){
                    case GeneratorFromDB.SearchCriteria.Equal:
                        returnValue =returnValue.Where(it=>it.Text==null);
                        continue;
                    case GeneratorFromDB.SearchCriteria.Different:
                        returnValue =returnValue.Where(it=>it.Text!=null);
                        continue;
                    default:
                        throw new ArgumentException($"null cannot have {(int)s.Criteria} {s.Criteria} for {s.FieldName}");
                    }
                
            
                                }//end if s.value is null -search for null
        //if we are here, s.Value is not null
        { //use this to define value in smaller scope
                                var value = s.Value;
                var valueArray= s.Value?.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .ToArray();
                ;
          
          
        switch(s.Criteria){

            case GeneratorFromDB.SearchCriteria.Between:
                if(valueArray?.Length != 2){
                    throw new ArgumentException("between must have 2 args, separated by comma => value is:" + s.Value);
                }
                                    returnValue =returnValue.Where(it=>String.Compare(it.Text,valueArray[0]) >= 0  && String.Compare(it.Text,valueArray[1]) <= 0);
                  
                continue;
            case GeneratorFromDB.SearchCriteria.NotBetween:
            
            if(valueArray?.Length != 2){
                    throw new ArgumentException("not between must have 2 args, separated by comma => value is:" + s.Value);
                }
                                    returnValue =returnValue.Where(it=>String.Compare(it.Text,valueArray[0]) < 0  || String.Compare(it.Text,valueArray[1]) > 0);
                  
                continue;
            case GeneratorFromDB.SearchCriteria.InArray:
                        returnValue =returnValue.Where(it=> valueArray!.Contains(it.Text));
                  
                continue;
            case GeneratorFromDB.SearchCriteria.NotInArray:
                        returnValue =returnValue.Where(it=> !valueArray!.Contains(it.Text));
                  
                continue;
            case GeneratorFromDB.SearchCriteria.Equal:
                returnValue =returnValue.Where(it=>it.Text==value);
                continue;
            case GeneratorFromDB.SearchCriteria.Different:
                returnValue =returnValue.Where(it=>it.Text!=value);
                continue;
            
            case GeneratorFromDB.SearchCriteria.Less:
                                                    returnValue =returnValue.Where(it=>String.Compare(it.Text,value) < 0 );
                                                
                        continue;
                    case GeneratorFromDB.SearchCriteria.LessOrEqual:
                                                    returnValue =returnValue.Where(it=>String.Compare(it.Text,value) <= 0 );
                                                
                        continue;
                    case GeneratorFromDB.SearchCriteria.Greater:
                                                    returnValue =returnValue.Where(it=>String.Compare(it.Text,value) > 0 );
                                                continue;
                    case GeneratorFromDB.SearchCriteria.GreaterOrEqual:
                                                    returnValue =returnValue.Where(it=>String.Compare(it.Text,value) >= 0 );
                                                continue;
                                            case GeneratorFromDB.SearchCriteria.Contains:
                        returnValue =returnValue.Where(it=>it.Text != null && it.Text.Contains(value));
                        continue;
                    case GeneratorFromDB.SearchCriteria.StartsWith:
                        returnValue =returnValue.Where(it=>it.Text != null &&  it.Text.StartsWith(value));
                        continue;
                    case GeneratorFromDB.SearchCriteria.EndsWith:
                        returnValue =returnValue.Where(it=>it.Text != null && it.Text.EndsWith(value));
                        continue;
                    /*case SearchCriteria.Contains:
                        returnValue =returnValue.Where(it=> it.Text != null && it.Text.Contains(value));
                        continue;
                    */
                                                            

            default:
                throw new ArgumentException($"not found Criteria {(int)s.Criteria} {s.Criteria} for {s.FieldName}");
        }//end switch after s.Criteria

                //continue;
        } //end use this to define value in smaller scope
            
                    
            case eTILT_NoteColumns.Link:
                //string isNullable True
                if(s.Value == null)
        {
                            switch(s.Criteria){
                    case GeneratorFromDB.SearchCriteria.Equal:
                        returnValue =returnValue.Where(it=>it.Link==null);
                        continue;
                    case GeneratorFromDB.SearchCriteria.Different:
                        returnValue =returnValue.Where(it=>it.Link!=null);
                        continue;
                    default:
                        throw new ArgumentException($"null cannot have {(int)s.Criteria} {s.Criteria} for {s.FieldName}");
                    }
                
            
                                }//end if s.value is null -search for null
        //if we are here, s.Value is not null
        { //use this to define value in smaller scope
                                var value = s.Value;
                var valueArray= s.Value?.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .ToArray();
                ;
          
          
        switch(s.Criteria){

            case GeneratorFromDB.SearchCriteria.Between:
                if(valueArray?.Length != 2){
                    throw new ArgumentException("between must have 2 args, separated by comma => value is:" + s.Value);
                }
                                    returnValue =returnValue.Where(it=>String.Compare(it.Link,valueArray[0]) >= 0  && String.Compare(it.Link,valueArray[1]) <= 0);
                  
                continue;
            case GeneratorFromDB.SearchCriteria.NotBetween:
            
            if(valueArray?.Length != 2){
                    throw new ArgumentException("not between must have 2 args, separated by comma => value is:" + s.Value);
                }
                                    returnValue =returnValue.Where(it=>String.Compare(it.Link,valueArray[0]) < 0  || String.Compare(it.Link,valueArray[1]) > 0);
                  
                continue;
            case GeneratorFromDB.SearchCriteria.InArray:
                        returnValue =returnValue.Where(it=> valueArray!.Contains(it.Link));
                  
                continue;
            case GeneratorFromDB.SearchCriteria.NotInArray:
                        returnValue =returnValue.Where(it=> !valueArray!.Contains(it.Link));
                  
                continue;
            case GeneratorFromDB.SearchCriteria.Equal:
                returnValue =returnValue.Where(it=>it.Link==value);
                continue;
            case GeneratorFromDB.SearchCriteria.Different:
                returnValue =returnValue.Where(it=>it.Link!=value);
                continue;
            
            case GeneratorFromDB.SearchCriteria.Less:
                                                    returnValue =returnValue.Where(it=>String.Compare(it.Link,value) < 0 );
                                                
                        continue;
                    case GeneratorFromDB.SearchCriteria.LessOrEqual:
                                                    returnValue =returnValue.Where(it=>String.Compare(it.Link,value) <= 0 );
                                                
                        continue;
                    case GeneratorFromDB.SearchCriteria.Greater:
                                                    returnValue =returnValue.Where(it=>String.Compare(it.Link,value) > 0 );
                                                continue;
                    case GeneratorFromDB.SearchCriteria.GreaterOrEqual:
                                                    returnValue =returnValue.Where(it=>String.Compare(it.Link,value) >= 0 );
                                                continue;
                                            case GeneratorFromDB.SearchCriteria.Contains:
                        returnValue =returnValue.Where(it=>it.Link != null && it.Link.Contains(value));
                        continue;
                    case GeneratorFromDB.SearchCriteria.StartsWith:
                        returnValue =returnValue.Where(it=>it.Link != null &&  it.Link.StartsWith(value));
                        continue;
                    case GeneratorFromDB.SearchCriteria.EndsWith:
                        returnValue =returnValue.Where(it=>it.Link != null && it.Link.EndsWith(value));
                        continue;
                    /*case SearchCriteria.Contains:
                        returnValue =returnValue.Where(it=> it.Link != null && it.Link.Contains(value));
                        continue;
                    */
                                                            

            default:
                throw new ArgumentException($"not found Criteria {(int)s.Criteria} {s.Criteria} for {s.FieldName}");
        }//end switch after s.Criteria

                //continue;
        } //end use this to define value in smaller scope
            
                    
            case eTILT_NoteColumns.ForDate:
                //DateTime? isNullable True
                if(s.Value == null)
        {
                            switch(s.Criteria){
                    case GeneratorFromDB.SearchCriteria.Equal:
                        returnValue =returnValue.Where(it=>it.ForDate==null);
                        continue;
                    case GeneratorFromDB.SearchCriteria.Different:
                        returnValue =returnValue.Where(it=>it.ForDate!=null);
                        continue;
                    default:
                        throw new ArgumentException($"null cannot have {(int)s.Criteria} {s.Criteria} for {s.FieldName}");
                    }
                
            
                                }//end if s.value is null -search for null
        //if we are here, s.Value is not null
        { //use this to define value in smaller scope
                        var valueArray = s.Value
                    .Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(it=>DateTime.Parse(it))
                    .ToArray();
                var value = valueArray[0];
                  
          
        switch(s.Criteria){

            case GeneratorFromDB.SearchCriteria.Between:
                if(valueArray?.Length != 2){
                    throw new ArgumentException("between must have 2 args, separated by comma => value is:" + s.Value);
                }
                                    returnValue =returnValue.Where(it=>it.ForDate >= valueArray[0] && it.ForDate <= valueArray[1]);
                  
                continue;
            case GeneratorFromDB.SearchCriteria.NotBetween:
            
            if(valueArray?.Length != 2){
                    throw new ArgumentException("not between must have 2 args, separated by comma => value is:" + s.Value);
                }
                                    returnValue =returnValue.Where(it=>it.ForDate < valueArray[0] || it.ForDate > valueArray[1]);
                  
                continue;
            case GeneratorFromDB.SearchCriteria.InArray:
                        returnValue =returnValue.Where(it=>it.ForDate != null && valueArray!.Contains(it.ForDate.Value));
                  
                continue;
            case GeneratorFromDB.SearchCriteria.NotInArray:
                        returnValue =returnValue.Where(it=>it.ForDate != null && !valueArray!.Contains(it.ForDate.Value));
                  
                continue;
            case GeneratorFromDB.SearchCriteria.Equal:
                returnValue =returnValue.Where(it=>it.ForDate==value);
                continue;
            case GeneratorFromDB.SearchCriteria.Different:
                returnValue =returnValue.Where(it=>it.ForDate!=value);
                continue;
            
            case GeneratorFromDB.SearchCriteria.Less:
                                                returnValue =returnValue.Where(it=>it.ForDate<value);
                                                
                        continue;
                    case GeneratorFromDB.SearchCriteria.LessOrEqual:
                                                
                        returnValue =returnValue.Where(it=>it.ForDate<=value);
                                                
                        continue;
                    case GeneratorFromDB.SearchCriteria.Greater:
                                                
                        returnValue =returnValue.Where(it=>it.ForDate>value);
                                                continue;
                    case GeneratorFromDB.SearchCriteria.GreaterOrEqual:
                                                returnValue =returnValue.Where(it=>it.ForDate>=value);
                                                continue;
                                                case GeneratorFromDB.SearchCriteria.EqualYear:
            {//for variable year
                var year=  value.Year;
                var yearStart=new DateTime(year,1,1);
                var yearEnd=new DateTime(year+1,1,1);
                returnValue =returnValue.Where(it=>it.ForDate>=yearStart && it.ForDate<yearEnd);
                }//end for variable year
                continue;
            case GeneratorFromDB.SearchCriteria.DifferentYear:
            {//for variable year
                var year=  value.Year;
                var yearStart=new DateTime(year,1,1);
                var yearEnd=new DateTime(year+1,1,1);
                returnValue =returnValue.Where(it=>it.ForDate<yearStart || it.ForDate>=yearEnd);
                }//end for variable year
                continue;
            case GeneratorFromDB.SearchCriteria.GreaterYear:
            {//for variable year
                var year=  value.Year;
                
                var yearStart=new DateTime(year+1,1,1);
                returnValue =returnValue.Where(it=>it.ForDate > yearStart);
                }//end for variable year
                continue;
            case GeneratorFromDB.SearchCriteria.GreaterOrEqualYear:
            {//for variable year
                var year=  value.Year;
                
                var yearStart=new DateTime(year,1,1);
                returnValue =returnValue.Where(it=>it.ForDate > yearStart);
                }//end for variable year
                continue;
                    
            case GeneratorFromDB.SearchCriteria.LessYear:
            {//for variable year
                var year=  value.Year;
                
                var yearEnd=new DateTime(year,1,1).AddMicroseconds(-1);
                returnValue =returnValue.Where(it=>it.ForDate <= yearEnd);
                }//end for variable year
                continue;
            case GeneratorFromDB.SearchCriteria.LessOrEqualYear:
            {//for variable year
                var year=  value.Year;
                
                var yearEnd=new DateTime(year+1,1,1).AddMicroseconds(-1);
                returnValue =returnValue.Where(it=>it.ForDate <= yearEnd);
                }//end for variable year
                continue;

                                                

            default:
                throw new ArgumentException($"not found Criteria {(int)s.Criteria} {s.Criteria} for {s.FieldName}");
        }//end switch after s.Criteria

                //continue;
        } //end use this to define value in smaller scope
            
                    
            case eTILT_NoteColumns.TimeZoneString:
                //string isNullable True
                if(s.Value == null)
        {
                            switch(s.Criteria){
                    case GeneratorFromDB.SearchCriteria.Equal:
                        returnValue =returnValue.Where(it=>it.TimeZoneString==null);
                        continue;
                    case GeneratorFromDB.SearchCriteria.Different:
                        returnValue =returnValue.Where(it=>it.TimeZoneString!=null);
                        continue;
                    default:
                        throw new ArgumentException($"null cannot have {(int)s.Criteria} {s.Criteria} for {s.FieldName}");
                    }
                
            
                                }//end if s.value is null -search for null
        //if we are here, s.Value is not null
        { //use this to define value in smaller scope
                                var value = s.Value;
                var valueArray= s.Value?.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .ToArray();
                ;
          
          
        switch(s.Criteria){

            case GeneratorFromDB.SearchCriteria.Between:
                if(valueArray?.Length != 2){
                    throw new ArgumentException("between must have 2 args, separated by comma => value is:" + s.Value);
                }
                                    returnValue =returnValue.Where(it=>String.Compare(it.TimeZoneString,valueArray[0]) >= 0  && String.Compare(it.TimeZoneString,valueArray[1]) <= 0);
                  
                continue;
            case GeneratorFromDB.SearchCriteria.NotBetween:
            
            if(valueArray?.Length != 2){
                    throw new ArgumentException("not between must have 2 args, separated by comma => value is:" + s.Value);
                }
                                    returnValue =returnValue.Where(it=>String.Compare(it.TimeZoneString,valueArray[0]) < 0  || String.Compare(it.TimeZoneString,valueArray[1]) > 0);
                  
                continue;
            case GeneratorFromDB.SearchCriteria.InArray:
                        returnValue =returnValue.Where(it=> valueArray!.Contains(it.TimeZoneString));
                  
                continue;
            case GeneratorFromDB.SearchCriteria.NotInArray:
                        returnValue =returnValue.Where(it=> !valueArray!.Contains(it.TimeZoneString));
                  
                continue;
            case GeneratorFromDB.SearchCriteria.Equal:
                returnValue =returnValue.Where(it=>it.TimeZoneString==value);
                continue;
            case GeneratorFromDB.SearchCriteria.Different:
                returnValue =returnValue.Where(it=>it.TimeZoneString!=value);
                continue;
            
            case GeneratorFromDB.SearchCriteria.Less:
                                                    returnValue =returnValue.Where(it=>String.Compare(it.TimeZoneString,value) < 0 );
                                                
                        continue;
                    case GeneratorFromDB.SearchCriteria.LessOrEqual:
                                                    returnValue =returnValue.Where(it=>String.Compare(it.TimeZoneString,value) <= 0 );
                                                
                        continue;
                    case GeneratorFromDB.SearchCriteria.Greater:
                                                    returnValue =returnValue.Where(it=>String.Compare(it.TimeZoneString,value) > 0 );
                                                continue;
                    case GeneratorFromDB.SearchCriteria.GreaterOrEqual:
                                                    returnValue =returnValue.Where(it=>String.Compare(it.TimeZoneString,value) >= 0 );
                                                continue;
                                            case GeneratorFromDB.SearchCriteria.Contains:
                        returnValue =returnValue.Where(it=>it.TimeZoneString != null && it.TimeZoneString.Contains(value));
                        continue;
                    case GeneratorFromDB.SearchCriteria.StartsWith:
                        returnValue =returnValue.Where(it=>it.TimeZoneString != null &&  it.TimeZoneString.StartsWith(value));
                        continue;
                    case GeneratorFromDB.SearchCriteria.EndsWith:
                        returnValue =returnValue.Where(it=>it.TimeZoneString != null && it.TimeZoneString.EndsWith(value));
                        continue;
                    /*case SearchCriteria.Contains:
                        returnValue =returnValue.Where(it=> it.TimeZoneString != null && it.TimeZoneString.Contains(value));
                        continue;
                    */
                                                            

            default:
                throw new ArgumentException($"not found Criteria {(int)s.Criteria} {s.Criteria} for {s.FieldName}");
        }//end switch after s.Criteria

                //continue;
        } //end use this to define value in smaller scope
            
                }//end switch  
       }//end foreach
    return returnValue;
    //throw new NotImplementedException("not");
    }

    public long ID { get; set; }

    public long IDURL { get; set; }

    public string Text { get; set; } = null!;

    public string? Link { get; set; }

    public DateTime? ForDate { get; set; }

    public string? TimeZoneString { get; set; }
}
