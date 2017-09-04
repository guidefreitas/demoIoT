package com.emb2sociec.demoiotmobile;

import android.content.Context;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v7.widget.GridLayoutManager;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.emb2sociec.demoiotmobile.dummy.DummyContent;
import com.emb2sociec.demoiotmobile.dummy.DummyContent.DeviceItem;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;

/**
 * A fragment representing a list of Items.
 * <p/>
 * Activities containing this fragment MUST implement the {@link OnListFragmentInteractionListener}
 * interface.
 */
public class DeviceFragment extends Fragment {

    private OnListFragmentInteractionListener mListener;
    private DeviceViewAdapter adapter;
    private RecyclerView recyclerView;

    public DeviceFragment() {
    }

    // TODO: Customize parameter initialization
    @SuppressWarnings("unused")
    public static DeviceFragment newInstance(int columnCount) {
        DeviceFragment fragment = new DeviceFragment();
        return fragment;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_device_list, container, false);

        // Set the adapter
        if (view instanceof RecyclerView) {
            Context context = view.getContext();
            recyclerView = (RecyclerView) view;
            recyclerView.setLayoutManager(new LinearLayoutManager(context));

            APIManager.getInstance().getDevices(new Response.Listener<JSONObject>() {
                @Override
                public void onResponse(JSONObject response) {
                    try{
                        ArrayList<DeviceItem> items = new ArrayList<DeviceItem>();
                        JSONArray data = response.getJSONArray("Data");
                        for(int i=0;i<data.length();i++){
                            JSONObject obj = data.getJSONObject(i);
                            String description = obj.getString("Description");
                            String devId = obj.getString("Id");
                            String serialNumber = obj.getString("SerialNumber");
                            DeviceItem devItem = new DeviceItem(devId, description, serialNumber);
                            items.add(devItem);
                        }
                        adapter = new DeviceViewAdapter(items, mListener);
                        recyclerView.setAdapter(adapter);
                    }catch(Exception ex){
                        Log.e("DeviceFragment", "Error loading data");
                    }

                }
            }, new Response.ErrorListener() {
                @Override
                public void onErrorResponse(VolleyError error) {
                    Log.e("DeviceFragment", "Error loading data");
                }
            });
        }
        return view;
    }


    @Override
    public void onAttach(Context context) {
        super.onAttach(context);
        if (context instanceof OnListFragmentInteractionListener) {
            mListener = (OnListFragmentInteractionListener) context;
        } else {
            throw new RuntimeException(context.toString()
                    + " must implement OnListFragmentInteractionListener");
        }

        //adapter = new DeviceViewAdapter(DummyContent.ITEMS, mListener);
        //recyclerView.setAdapter(adapter);
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }


    public interface OnListFragmentInteractionListener {
        void onListFragmentInteraction(DeviceItem item);
    }
}
