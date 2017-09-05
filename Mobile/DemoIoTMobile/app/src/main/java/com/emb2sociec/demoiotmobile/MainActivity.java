package com.emb2sociec.demoiotmobile;

import android.support.v4.app.FragmentTransaction;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;

public class MainActivity extends AppCompatActivity implements DeviceFragment.OnListFragmentInteractionListener, UpdateFragment.OnListFragmentInteractionListener {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        setTitle(R.string.app_name);
        DeviceFragment deviceFragment = new DeviceFragment();
        FragmentTransaction ft = getSupportFragmentManager().beginTransaction();
        ft.replace(R.id.frag_place, deviceFragment);
        ft.commit();
    }

    @Override
    public void onListFragmentInteraction(DeviceItem item) {
        Log.i("MainActivity", "Device clicked");
        UpdateFragment updateFragment = UpdateFragment.newInstance(item.id);
        FragmentTransaction ft = getSupportFragmentManager().beginTransaction();
        ft.replace(R.id.frag_place, updateFragment).addToBackStack("deviceUpdates" + item.id);
        ft.commit();
    }

    @Override
    public void onListFragmentInteraction(UpdateItem item) {
        Log.i("UpdateFragment", "Update clicked");
    }
}
