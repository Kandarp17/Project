package com.example.patel_000.myproject;

import android.app.Activity;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import com.example.patel_000.myproject.R;

import java.util.ArrayList;
import java.util.HashMap;

/**
 * Created by patel_000 on 13-02-2017.
 */

public class ListAdp {
    private Activity activity;
    private ArrayList <String> data;
    private static LayoutInflater inflater=null;


    public ListAdp(Activity a, ArrayList <String> d) {
        activity = a;
        data=d;
        inflater = (LayoutInflater)activity.getSystemService(Context.LAYOUT_INFLATER_SERVICE);

    }

    public int getCount() {
        return data.size();
    }

    public Object getItem(int position) {
        return position;
    }

    public long getItemId(int position) {
        return position;
    }

    public View getView(int position, View convertView, ViewGroup parent) {
        View vi=convertView;
        if(convertView==null)
            vi = inflater.inflate(R.layout.list_row, null);

        TextView title = (TextView)vi.findViewById(R.id.title); // title



        // Setting all values in listview

        return vi;
    }
}
