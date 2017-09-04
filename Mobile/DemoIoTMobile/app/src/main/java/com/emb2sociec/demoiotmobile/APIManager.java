package com.emb2sociec.demoiotmobile;

import android.content.Context;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.Volley;

import org.json.JSONObject;

/**
 * Created by guilherme on 04/09/17.
 */

public final class APIManager {
    private static APIManager instance;
    String deviceUrl = "http://demoiotsociesc.azurewebsites.net/api/device";

    RequestQueue queue = Volley.newRequestQueue(DemoApplication.getAppContext());



    public static APIManager getInstance(){
        if(instance == null){
            instance = new APIManager();
        }
        return instance;
    }

    public void getDevices(Response.Listener<JSONObject> response, Response.ErrorListener errorListener){
        JsonObjectRequest jsObjRequest = new JsonObjectRequest
                (Request.Method.GET, deviceUrl, null, response, errorListener);
        queue.add(jsObjRequest);
    }
}
