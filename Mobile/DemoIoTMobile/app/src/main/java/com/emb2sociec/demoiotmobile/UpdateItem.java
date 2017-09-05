package com.emb2sociec.demoiotmobile;

import java.util.Date;

/**
 * Created by guilherme on 04/09/17.
 */

public class UpdateItem {
    public String id;
    public Date date;
    public String value;

    public UpdateItem(String id, Date date, String value){
        this.id = id;
        this.date = date;
        this.value = value;
    }
}
