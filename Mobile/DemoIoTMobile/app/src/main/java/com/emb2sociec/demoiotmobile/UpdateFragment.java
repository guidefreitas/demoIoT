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

import org.json.JSONArray;
import org.json.JSONObject;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.Locale;

/**
 * A fragment representing a list of Items.
 * <p/>
 * Activities containing this fragment MUST implement the {@link OnListFragmentInteractionListener}
 * interface.
 */
public class UpdateFragment extends Fragment {

    RecyclerView recyclerView;
    private UpdateViewAdapter adapter;
    String deviceId;
    private OnListFragmentInteractionListener mListener;

    /**
     * Mandatory empty constructor for the fragment manager to instantiate the
     * fragment (e.g. upon screen orientation changes).
     */
    public UpdateFragment() {
    }

    public static UpdateFragment newInstance(String deviceId) {
        UpdateFragment fragment = new UpdateFragment();
        Bundle args = new Bundle();
        args.putString("deviceId", deviceId);
        fragment.setArguments(args);
        return fragment;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_update_list, container, false);

        Bundle args = getArguments();
        deviceId = args.getString("deviceId");

        getActivity().setTitle("Device " + deviceId);

        // Set the adapter
        if (view instanceof RecyclerView) {
            Context context = view.getContext();
            recyclerView = (RecyclerView) view;
            recyclerView.setLayoutManager(new LinearLayoutManager(context));

            APIManager.getInstance().getDeviceUpdates(deviceId, new Response.Listener<JSONObject>() {
                @Override
                public void onResponse(JSONObject response) {
                    try{
                        Log.i("Response", response.toString());
                        ArrayList<UpdateItem> items = new ArrayList<UpdateItem>();
                        JSONArray data = response.getJSONArray("Updates");
                        for(int i=0;i<data.length();i++){
                            JSONObject obj = data.getJSONObject(i);
                            String value = obj.getString("Value");
                            String devId = obj.getString("Id");
                            String dateTimeString = obj.getString("DateTime");
                            DateFormat df = new SimpleDateFormat("dd/MM/yyyy HH:mm:ss", Locale.ENGLISH);
                            Date dateTime =  df.parse(dateTimeString);
                            UpdateItem updateItem = new UpdateItem(devId, dateTime, value);
                            items.add(updateItem);
                        }
                        adapter = new UpdateViewAdapter(items, mListener);
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
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    /**
     * This interface must be implemented by activities that contain this
     * fragment to allow an interaction in this fragment to be communicated
     * to the activity and potentially other fragments contained in that
     * activity.
     * <p/>
     * See the Android Training lesson <a href=
     * "http://developer.android.com/training/basics/fragments/communicating.html"
     * >Communicating with Other Fragments</a> for more information.
     */
    public interface OnListFragmentInteractionListener {
        // TODO: Update argument type and name
        void onListFragmentInteraction(UpdateItem item);
    }
}
