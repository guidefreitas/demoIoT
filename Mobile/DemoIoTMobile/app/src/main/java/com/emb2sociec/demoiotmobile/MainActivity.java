package com.emb2sociec.demoiotmobile;

import android.os.AsyncTask;
import android.support.v4.app.FragmentTransaction;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;

import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.emb2sociec.demoiotmobile.dummy.DummyContent;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

public class MainActivity extends AppCompatActivity implements DeviceFragment.OnListFragmentInteractionListener {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        DeviceFragment deviceFragment = new DeviceFragment();
        FragmentTransaction ft = getSupportFragmentManager().beginTransaction();
        ft.replace(R.id.frag_place, deviceFragment);
        ft.commit();

        Log.i("Teste1", "Teste1");
    }

    @Override
    public void onListFragmentInteraction(DummyContent.DeviceItem item) {
        Log.i("Teste2", "Teste2");

    }



}
